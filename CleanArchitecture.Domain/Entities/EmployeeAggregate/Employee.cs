using CleanArchitecture.Domain.Shared;
using System.Net.Http.Headers;

namespace CleanArchitecture.Domain.Entities.EmployeeAggregate
{
    public class Employee : AgreggateRoot<Guid>
    {
        public IReadOnlyCollection<Contract> Contracts => _contracts;
        private List<Contract> _contracts = new();
        public Contract CurrentContract => _contracts.OrderByDescending(c => c.AdmissionDate).First();

        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime Birth { get; private set; }
        public Guid CompanyId { get; private set; }

        private Employee(Guid id, string name, string email, DateTime birth, Guid companyId)
        {
            Id = id;
            Name = name;
            Email = email;
            Birth = birth;
            CompanyId = companyId;
        }

        public static Employee Create(string name, string email, DateTime birth, Guid companyId)
        {
            if (birth > DateTime.Now)
            {
                throw new Exception("Date birth cannot be in future"); // mudar aqui tambem depois
            }

            return new Employee(Guid.NewGuid(), name, email, birth, companyId);
        }

        public Contract AddContract(DateTime admissionDate, int firstProbationPeriodDays,
            int secondProbationPeriodDays, decimal salary, Guid? managerId)
        {
            var contract = Contract.Create(admissionDate, firstProbationPeriodDays, secondProbationPeriodDays, salary, managerId);
            _contracts.Add(contract);
            return contract;
        }

        public void ApproveFirstProbationPeriod()
        {
            CurrentContract.ApproveFirstProbationPeriod();
        }

        public void ApproveSecondProbationPeriod()
        {
            CurrentContract.ApproveSecondProbationPeriod();
        }
    }

}
