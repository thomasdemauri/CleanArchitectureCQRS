using CleanArchitecture.Domain.Core.Data;
using DomainCompany = CleanArchitecture.Domain.Entities.CompanyAggregate.Company;

namespace CleanArchitecture.Domain.Entities.CompanyAggregate;

public interface ICompanyRepository : IRepository<DomainCompany, Guid>
{
}
