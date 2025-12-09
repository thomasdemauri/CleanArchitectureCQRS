using Contracts.HR.Company;
using EventBus.Abstractions;

namespace CleanArchitectureCQRS.API.IntegrationEventHandler
{
    public class CompanyCreatedIntegrationEventHandler : IIntegrationEventHandler<CompanyCreatedIntegrationEvent>
    {
        public Task Handle(CompanyCreatedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
