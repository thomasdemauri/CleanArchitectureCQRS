using CleanArchitecture.Application.Commands.Employee;
using CleanArchitecture.Application.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.CommandHandlers.Employee
{

    public class ApproveFirstProbationPeriodContractCommandHandler : 
        IRequestHandler<ApproveFirstProbationPeriodContractCommand, bool>
    {
        private readonly IEmployeeRepository _repository;

        public ApproveFirstProbationPeriodContractCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ApproveFirstProbationPeriodContractCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetById(request.EmployeeId);

            if (employee == null)
            {
                throw new Exception("Employee not found");
            }

            employee.ApproveFirstProbationPeriod();

            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
