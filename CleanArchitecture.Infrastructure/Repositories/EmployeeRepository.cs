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
            return await _context.Employees.Include(e => e.Contracts)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
