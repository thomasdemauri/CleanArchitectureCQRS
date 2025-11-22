using CleanArchitecture.Application.Commands.Employee;
using CleanArchitecture.Application.Interfaces;
using MediatR;
using DomainEmployee = CleanArchitecture.Domain.Entities.EmployeeAggregate.Employee;


namespace CleanArchitecture.Application.CommandHandlers.Employee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = DomainEmployee.Create(
                request.Name,
                request.Email,
                request.Birth,
                request.CompanyId);

            await _employeeRepository.Add(employee);
            await _employeeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            
            return employee.Id;
        }
    }
}
