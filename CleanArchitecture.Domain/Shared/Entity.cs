using MediatR;

namespace CleanArchitecture.Domain.Shared
{
    public interface IEntity
    {
        public IReadOnlyCollection<INotification> DomainEvents { get; }
        public void ClearDomainEvents();
    }

    public abstract class Entity<Tkey> : IEntity
    {
        public Tkey Id { get; protected set; } = default!;

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
