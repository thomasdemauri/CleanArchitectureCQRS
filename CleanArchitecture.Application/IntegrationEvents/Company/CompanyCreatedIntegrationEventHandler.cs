using MediatR;

namespace CleanArchitecture.Application.IntegrationEvents.Company
{
    public sealed class CompanyCreatedIntegrationEventHandler : INotificationHandler<CompanyCreatedIntegrationEvent>
    {
        public Task Handle(CompanyCreatedIntegrationEvent notification, CancellationToken cancellationToken)
        {
            return Task.Delay(4000, cancellationToken);
        }
    }
}
