using EventBus.Events;

namespace CleanArchitecture.Application.Abstractions
{
    public interface IEventBus
    {
        public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IIntegrationEvent;
    }
}
