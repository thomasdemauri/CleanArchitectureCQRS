using EventBus.Events;

namespace EventBus.Abstractions
{
    public interface IEventBus
    {
        public Task PublishAsync(IntegrationEvent @event);
    }
}
