using CleanArchitecture.Application.Commands.Employee;
using CleanArchitecture.Application.Interfaces;
using MediatR;
using DomainEmployee = CleanArchitecture.Domain.Entities.Employee;


namespace CleanArchitecture.Application.CommandHandlers.Employee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        public const int ENTITY_NOT_SAVED = -1;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository)
        {
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.SaveChangesAsync();
            
            return employee.Id;
        }
    }
}
