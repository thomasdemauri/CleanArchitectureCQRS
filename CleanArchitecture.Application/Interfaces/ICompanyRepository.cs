using CleanArchitecture.Application.Interfaces;
using DomainCompany = CleanArchitecture.Domain.Entities.Company;

namespace CleanArchitecture.Application.Interfaces;

public interface ICompanyRepository : IRepository<DomainCompany, Guid>
{
}
