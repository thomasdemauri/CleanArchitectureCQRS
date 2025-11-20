using CleanArchitecture.Application.Queries;
using CleanArchitecture.Application.ViewModels.Employee;
using CleanArchitecture.Infrastructure.Queries.Resources;
using Dapper;
using System.Data;

namespace CleanArchitecture.Infrastructure.Queries
{
    public class EmployeeQueries : IEmployeeQueries
    {
        private IDbConnection _dbConnection;

        public EmployeeQueries(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<EmployeeDetailedViewModel> GetById(Guid? id)
        {
            return await _dbConnection.QuerySingleAsync<EmployeeDetailedViewModel>(QueriesResource.EmployeeDetailed, new { Id = id });
        }

    }
}
