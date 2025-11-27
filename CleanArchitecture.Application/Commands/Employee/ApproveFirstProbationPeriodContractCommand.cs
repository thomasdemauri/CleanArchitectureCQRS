using MediatR;

namespace CleanArchitecture.Application.Commands.Employee
{
    public class ApproveFirstProbationPeriodContractCommand : IRequest<bool>
    {
        public ApproveFirstProbationPeriodContractCommand(Guid employeeId)
        {
            EmployeeId = employeeId;
        }

        public Guid EmployeeId { get; set; }
    }
}
