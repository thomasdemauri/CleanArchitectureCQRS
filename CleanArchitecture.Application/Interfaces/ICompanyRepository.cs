using CleanArchitecture.Application.Interfaces;
using DomainCompany = CleanArchitecture.Domain.Entities.CompanyAggregate.Company;

namespace CleanArchitecture.Application.Interfaces;

public interface ICompanyRepository : IRepository<DomainCompany, Guid>
{
}
