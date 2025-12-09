using Autofac;
using Azure.Messaging.ServiceBus;
using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EventBusService.AzureBusService
{
    public class EventBusInstance : IEventBus
    {
        private readonly IServiceBusPersisterConnection _serviceBusPersisterConnection;
        //private readonly ILifetimeScope _autoFac;
        private readonly ILogger<EventBusInstance> _logger;

        private ServiceBusSender _sender;
        private ServiceBusProcessor _processor;

        private readonly string _topicName;
        private readonly string _subscription;
        private const string INTEGRATION_EVENT_SUFFIX = "IntegrationEvent";


        public EventBusInstance(
            IServiceBusPersisterConnection serviceBusPersisterConnection,
            ILogger<EventBusInstance> logger, string topicName, string subscription)
        {
            _topicName = topicName;
            _serviceBusPersisterConnection = serviceBusPersisterConnection;
            _sender = _serviceBusPersisterConnection.Client.CreateSender(_topicName);
            _logger = logger;
            _subscription = subscription;

            _logger.LogInformation($"🔧 Configurando EventBus:");
            _logger.LogInformation($"   Topic: {_topicName}");
            _logger.LogInformation($"   Subscription: {_subscription}");

            ServiceBusProcessorOptions options = new() { MaxConcurrentCalls = 1, AutoCompleteMessages = false };
            _processor = _serviceBusPersisterConnection.Client.CreateProcessor(_topicName, _subscription, options);

            StartProcess().GetAwaiter().GetResult();
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
            _logger.LogInformation($"📤 Publicando '{eventName}' (ID: {message.MessageId})");
            _logger.LogDebug($"   Payload: {jsonMessage}");

            try
            {
                await _sender.SendMessageAsync(message);
                _logger.LogInformation($"✅ Publicado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"❌ Erro ao publicar '{eventName}'");
                throw;
            }
        }

        public async Task StartProcess()
        {
            try
            {
                _logger.LogInformation($"🔵 Registrando handlers para topic '{_topicName}', subscription '{_subscription}'");

                _processor.ProcessMessageAsync += MessageHandler;
                _processor.ProcessErrorAsync += ErrorHandler;

                _logger.LogInformation("🔵 Iniciando processamento...");
                await _processor.StartProcessingAsync();

                _logger.LogInformation("✅ Processador iniciado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error starting message processor: {ex.Message}");
            }
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            _logger.LogInformation($"Received: {body} from subscription: <{_subscription}>");

            await args.CompleteMessageAsync(args.Message);
        }

        public Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogError(args.Exception, $"Error handling message: {args.Exception.Message}");
            return Task.CompletedTask;
        }
    }
}
