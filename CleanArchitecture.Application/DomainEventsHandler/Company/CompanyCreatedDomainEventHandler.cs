using CleanArchitecture.Domain.DomainEvents.Company;
using Contracts.HR.Company;
using EventBus.Abstractions;
using MediatR;

namespace CleanArchitecture.Application.DomainEventsHandler.Company
{
    public class CompanyCreatedDomainEventHandler : INotificationHandler<CompanyCreatedDomainEvent>
    {
        public readonly IEventBus _eventBus;

        public CompanyCreatedDomainEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(CompanyCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var @event = new CompanyCreatedIntegrationEvent(
                notification.Name, notification.RegisterNumber, notification.EstablishedOn);

            _eventBus.PublishAsync(@event);

            return Task.CompletedTask;
        }
    }
}
