using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities.EmployeeAggregate;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<Employee?> GetById(Guid id)
        {
            var employee = await base.GetById(id);
            if (employee == null)
                return null;

            _context.Entry(employee).Collection(e => e.Contracts).Load();

            return employee;
        }
    }
}
