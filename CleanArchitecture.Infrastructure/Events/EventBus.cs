using CleanArchitecture.Application.Abstractions.Events;

namespace CleanArchitecture.Infrastructure.Events
{
    public class EventBus : IEventBus
    {
        public IInMemoryMessageQueue _queue;

        public EventBus(IInMemoryMessageQueue queue)
        {
            _queue = queue;
        }

        public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken)
            where T : class, IIntegrationEvent
        {
            await _queue.Writer.WriteAsync(@event, cancellationToken);
        }
    }
}
