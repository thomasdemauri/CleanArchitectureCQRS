using CleanArchitecture.Application.Commands.Employee;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.CommandHandlers.Employee
{
    public class CreateEmployeeContractCommandHandler : IRequestHandler<CreateEmployeeContractCommand, Guid>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<CreateEmployeeContractCommandHandler> _logger;

        public CreateEmployeeContractCommandHandler(
            IEmployeeRepository employeeRepository, ILogger<CreateEmployeeContractCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateEmployeeContractCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetById(request.EmployeeId);

            if (employee == null)
            {
                throw new Exception();
            }

            var contract = employee.AddContract(
                request.AdmissionDate, request.FirstProbationPeriodDays,
                request.SecondProbationPeriodDays, request.Salary, request.ManagerId);

            await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Contract created successfully. Id: {contract.Id} Employee: {employee.Id} Name: {employee.Name}");

            return contract.Id;
        }
    }
}
