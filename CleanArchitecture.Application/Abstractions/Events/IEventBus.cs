namespace CleanArchitecture.Application.Abstractions.Events
{
    public interface IEventBus
    {
        public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IIntegrationEvent;
    }
}
