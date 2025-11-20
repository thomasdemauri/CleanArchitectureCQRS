using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
    }
}
