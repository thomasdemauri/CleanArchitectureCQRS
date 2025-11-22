namespace CleanArchitecture.Application.Requests.Employee
{
    public class AddEmployeeContractRequest
    {
        public DateTime AdmissionDate { get; set; }
        public int FirstProbationPeriodDays { get; set; } = 45;
        public int SecondProbationPeriodDays { get; set; } = 45;
        public decimal Salary { get; set; }
        public Guid ManagerId { get; set; }
    }
}
