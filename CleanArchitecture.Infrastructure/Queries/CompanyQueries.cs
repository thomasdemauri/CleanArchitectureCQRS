using CleanArchitecture.Application.Queries;
using CleanArchitecture.Application.ViewModels.Company;
using CleanArchitecture.Infrastructure.Queries.Resources;
using Dapper;
using System.Data;

namespace CleanArchitecture.Infrastructure.Queries
{
    public class CompanyQueries : ICompanyQueries

    {
        private IDbConnection _dbConnection;

        public CompanyQueries(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<CompanyViewModel>> GetAll()
        {
            Guid? id = null;
            return await _dbConnection.QueryAsync<CompanyViewModel>(QueriesResource.CompanyQuery, new { Id = id });
        }

        public async Task<CompanyDetailedViewModel> GetById(Guid? id)
        {
            return await _dbConnection.QuerySingleAsync<CompanyDetailedViewModel>(QueriesResource.CompanyQuery, new { Id = id });
        }
    }
}
