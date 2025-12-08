namespace EventBus.Events
{
    public interface IIntegrationEvent
    {
    }

    public abstract class IntegrationEvent : IIntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreationDate { get; } = DateTime.UtcNow;
    }
}
