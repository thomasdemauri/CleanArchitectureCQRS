using MediatR;

namespace CleanArchitecture.Application.Abstractions.Events
{
    public interface IIntegrationEvent : INotification
    {
        Guid Id { get; init; }
    }
}
