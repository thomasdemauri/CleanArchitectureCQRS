using CleanArchitecture.Application.ViewModels.Company;

namespace CleanArchitecture.Application.Queries
{
    public interface ICompanyQueries
    {
        public Task<IEnumerable<CompanyViewModel>> GetAll();
        public Task<CompanyDetailedViewModel> GetById(Guid? id);
    }
}
