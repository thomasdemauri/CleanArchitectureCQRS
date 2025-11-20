using CleanArchitecture.Application.ViewModels.Employee;

namespace CleanArchitecture.Application.Queries
{
    public interface IEmployeeQueries
    {
        public Task<EmployeeDetailedViewModel> GetById(Guid? id);

    }
}
