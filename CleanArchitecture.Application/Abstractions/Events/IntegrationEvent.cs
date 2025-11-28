namespace CleanArchitecture.Application.Abstractions.Events
{
    public abstract record IntegrationEvent(Guid Id) : IIntegrationEvent;
}
