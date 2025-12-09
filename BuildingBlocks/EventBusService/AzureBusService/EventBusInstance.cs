using Autofac;
using Azure.Messaging.ServiceBus;
using EventBus;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using System.Text.Json;

namespace EventBusService.AzureBusService
{
    public class EventBusInstance : IEventBus
    {
        private readonly IServiceBusPersisterConnection _serviceBusPersisterConnection;
        private readonly ILifetimeScope _autoFac;
        private readonly ILogger<EventBusInstance> _logger;

        private ServiceBusSender _sender;
        private ServiceBusProcessor _processor;
        private readonly IEventBusSubscriptionsManager _subscriptionsManager;

        private readonly string _topicName;
        private readonly string _subscriptionName;
        private const string INTEGRATION_EVENT_SUFFIX = "IntegrationEvent";
        private const string AUTOFAC_SCOPE_NAME = "autofac_event_scope";


        public EventBusInstance(
            IServiceBusPersisterConnection serviceBusPersisterConnection,
            ILogger<EventBusInstance> logger, string topicName, string subscription,
            IEventBusSubscriptionsManager subscriptionsManager, ILifetimeScope autoFac)
        {
            _topicName = topicName;
            _serviceBusPersisterConnection = serviceBusPersisterConnection;
            _sender = _serviceBusPersisterConnection.Client.CreateSender(_topicName);
            _logger = logger;
            _subscriptionName = subscription;
            _subscriptionsManager = subscriptionsManager;

            ServiceBusProcessorOptions options = new() { MaxConcurrentCalls = 1, AutoCompleteMessages = false };
            _processor = _serviceBusPersisterConnection.Client.CreateProcessor(_topicName, _subscriptionName, options);

            StartProcess().GetAwaiter().GetResult();
            _autoFac = autoFac;
        }

        public async Task PublishAsync(IntegrationEvent @event)
        {
            var eventName = @event.GetType().Name.Replace(INTEGRATION_EVENT_SUFFIX, "");
            var jsonMessage = JsonSerializer.Serialize(@event, @event.GetType());

            var message = new ServiceBusMessage
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = new BinaryData(jsonMessage),
                Subject = eventName
            };
            _logger.LogInformation($"Publicando '{eventName}' (ID: {message.MessageId})");
            _logger.LogDebug($"Payload: {jsonMessage}");

            try
            {
                await _sender.SendMessageAsync(message);
                _logger.LogInformation("Publicado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao publicar '{eventName}'");
                throw;
            }
        }

        public async Task StartProcess()
        {
            try
            {
                _logger.LogInformation($"Registrando handlers para topic '{_topicName}', subscription '{_subscriptionName}'");

                _processor.ProcessMessageAsync += MessageHandler;
                _processor.ProcessErrorAsync += ErrorHandler;

                _logger.LogInformation("Iniciando processamento...");
                await _processor.StartProcessingAsync();

                _logger.LogInformation("Processador iniciado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error starting message processor: {ex.Message}");
            }
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            var eventName = $"{args.Message.Subject}{INTEGRATION_EVENT_SUFFIX}";

            _logger.LogInformation($"Received: {body} from subscription: <{_subscriptionName}>");

            if (await ProcessEvent(eventName, body))
            {
                await args.CompleteMessageAsync(args.Message);  
            }
        }

        private async Task<bool> ProcessEvent(string eventName, string body)
        {
            var processed = false;

            if (_subscriptionsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autoFac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var handlers = _subscriptionsManager.GetHandlersForEvent(eventName);

                    foreach (var handler in handlers)
                    {
                        var handlerInstance = scope.ResolveOptional(handler.HandlerType);
                        if (handlerInstance == null) continue;

                        var eventType = _subscriptionsManager.GetEventTypeByName(eventName);
                        var integrationEvent = JsonSerializer.Deserialize(body, eventType);

                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handlerInstance, new object[] { integrationEvent });

                    }
                }

                processed = true;
            }

            return processed;
        }

        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, $"Error handling message: {args.Exception.Message}");
            return Task.CompletedTask;
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            _subscriptionsManager.AddSubscription<T, TH>();
        }
    }   
}
