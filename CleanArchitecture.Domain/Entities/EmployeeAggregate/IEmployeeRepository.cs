using CleanArchitecture.Domain.Core.Data;
using CleanArchitecture.Domain.Entities.EmployeeAggregate;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
    }
}
