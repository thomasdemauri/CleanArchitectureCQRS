using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Events
{
    public class IntegrationEventProcessorJob : BackgroundService
    {
        public readonly IInMemoryMessageQueue _queue;
        private readonly IPublisher _publisher;
        public readonly ILogger<IntegrationEventProcessorJob> _logger;

        public IntegrationEventProcessorJob(
            IInMemoryMessageQueue queue, 
            IPublisher publisher, 
            ILogger<IntegrationEventProcessorJob> logger)
        {
            _queue = queue;
            _publisher = publisher;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach(var integrationEvent in _queue.Reader.ReadAllAsync()) 
            {
                _logger.LogInformation("Publishing integration event {EventId} to mediator", integrationEvent.Id);

                await _publisher.Publish(integrationEvent, stoppingToken);

                _logger.LogInformation("Integration event {EventId} published to mediator", integrationEvent.Id);
            }
        }
    }
}
