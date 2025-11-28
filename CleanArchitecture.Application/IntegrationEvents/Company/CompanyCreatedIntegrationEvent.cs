using CleanArchitecture.Application.Abstractions.Events;
using MediatR;

namespace CleanArchitecture.Application.IntegrationEvents.Company
{
    public record CompanyCreatedIntegrationEvent(Guid Id,
        string Name, string RegisterNumber, DateTime EstablishedOn) : IntegrationEvent(Id)
    {
    }
}
