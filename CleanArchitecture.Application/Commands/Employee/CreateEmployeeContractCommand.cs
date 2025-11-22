using MediatR;

namespace CleanArchitecture.Application.Commands.Employee
{
    public class CreateEmployeeContractCommand : IRequest<Guid>
    {
        public DateTime AdmissionDate { get; set; }
        public int FirstProbationPeriodDays { get; set; } = 45;
        public int SecondProbationPeriodDays { get; set; } = 45;
        public decimal Salary {  get; set; }
        public Guid ManagerId { get; set; }
        public Guid EmployeeId { get; set; }

        public CreateEmployeeContractCommand(
            DateTime admissionDate, int firstProbationPeriodDays, int secondProbationPeriodDays,
            decimal salary, Guid managerId, Guid employeeId)
        {
            AdmissionDate = admissionDate;
            FirstProbationPeriodDays = firstProbationPeriodDays;
            SecondProbationPeriodDays = secondProbationPeriodDays;
            Salary = salary;
            ManagerId = managerId;
            EmployeeId = employeeId;
        }

    }
}
