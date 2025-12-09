using EventBus.Events;

namespace Contracts.HR.Company
{
    public class CompanyCreatedIntegrationEvent : IntegrationEvent
    {
        public CompanyCreatedIntegrationEvent(string name, string registerNumber, DateTime establishedOn)
        {
            Name = name;
            RegisterNumber = registerNumber;
            EstablishedOn = establishedOn;
        }

        public string Name { get; private set; }
        public string RegisterNumber { get; private set; }
        public DateTime EstablishedOn { get; private set; }
    }
}
