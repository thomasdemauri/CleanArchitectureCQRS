using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Exceptions.ContractExceptions;

namespace CleanArchitecture.Domain.Entities
{
    public class Contract : AgreggateRoot<Guid>
    {
        public DateTime AdmissionDate { get; private set; }
        public DateTime FirstProbationEndDate { get; private set; }
        public DateTime SecondProbationEndDate { get; private set; }
        public decimal Salary { get; private set; }
        public Guid? ManagerId { get; private set; }
        public bool IsActive { get; private set; }
        public bool? ApprovedFirstProbationPeriod { get; private set; }
        public bool? ApprovedSecondProbationPeriod { get; private set; }


        protected Contract() { }

        private Contract(
            Guid id, DateTime admissionDate, DateTime firstProbationEndDate,
            DateTime secondProbationEndDate, decimal salary, Guid? managerId)
        {
            AdmissionDate = admissionDate;
            FirstProbationEndDate = firstProbationEndDate;
            SecondProbationEndDate = secondProbationEndDate;
            Salary = salary;
            IsActive = true;

            if (managerId != null)
            {
                ManagerId = (Guid)managerId;
            }
        }

        public static Contract Create(
            Guid id, DateTime admissionDate, int firstProbationPeriodDays,
            int secondProbationPeriodDays, decimal salary, Guid? managerId)
        {
            if (admissionDate > DateTime.Now)
            {
                throw new AdmissionDateCannotBeInFutureException();
            }

            var firstProbationEndDate = admissionDate.AddDays(firstProbationPeriodDays);
            var secondProbationEndDate = firstProbationEndDate.AddDays(secondProbationPeriodDays);

            return new Contract(Guid.NewGuid(), admissionDate, firstProbationEndDate, secondProbationEndDate, salary, managerId);
        }

        public void ApproveFirstProbationPeriod()
        {
            if (DateTime.Now < FirstProbationEndDate)
            {
                throw new Exception("Cannot approve before first period end."); // lancar exception personalizada depois
            }

            ApprovedFirstProbationPeriod = true;
        }

        public void ApproveSecondProbationPeriod()
        {
            if (ApprovedFirstProbationPeriod != true && DateTime.Now < SecondProbationEndDate)
            {
                throw new Exception("Cannot approve before second period end."); // lancar exception personalizada depois
            }

            ApprovedSecondProbationPeriod = true;
        }
    }
}
