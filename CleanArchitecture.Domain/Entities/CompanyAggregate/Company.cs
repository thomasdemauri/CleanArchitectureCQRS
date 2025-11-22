using CleanArchitecture.Domain.Exceptions.CompanyExceptions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Entities.CompanyAggregate
{
    public class Company : AgreggateRoot<Guid>
    {
        public string Name { get; private set; }
        public string RegisterNumber { get; private set; }
        public DateTime EstablishedOn { get; private set; }

        private Company(Guid id, string name, string registerNumber, DateTime establishedOn)
        {
            Name = name;
            RegisterNumber = registerNumber;
            EstablishedOn = establishedOn;
        }

        public static Company Create(string name, string registerNumber, DateTime establishedOn)
        {
            var REGISTER_NUMBER_MAX_LENGTH = 14;

            if (string.IsNullOrEmpty(name))
            {
                throw new EmptyOrNullCompanyNameException();
            }

            if (registerNumber.Length != REGISTER_NUMBER_MAX_LENGTH)
            {
                throw new InvalidRegisterNumberException();
            }

            if (establishedOn > DateTime.Now)
            {
                throw new InvalidEstablishedOnDatetimeException();
            }

            return new Company(Guid.NewGuid(), name, registerNumber, establishedOn);
        }

        public void UpdateName(string newName)
        {

            if (string.IsNullOrEmpty(newName))
            {
                throw new EmptyOrNullCompanyNameException();
            }

            Name = newName;
        }

    }
}
