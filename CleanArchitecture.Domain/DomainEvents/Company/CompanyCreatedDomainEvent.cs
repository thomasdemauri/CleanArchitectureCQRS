using MediatR;

namespace CleanArchitecture.Domain.DomainEvents.Company
{
    public class CompanyCreatedDomainEvent : INotification
    {
        public CompanyCreatedDomainEvent(string name, string registerNumber, DateTime establishedOn)
        {
            Name = name;
            RegisterNumber = registerNumber;
            EstablishedOn = establishedOn;
        }

        public string Name { get; init; }
        public string RegisterNumber { get; init; }
        public DateTime EstablishedOn { get; init; }
    }
}
