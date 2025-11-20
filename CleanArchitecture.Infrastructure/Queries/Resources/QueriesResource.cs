using CleanArchitecture.Application.Queries.Resources;

namespace CleanArchitecture.Infrastructure.Queries.Resources
{
    public class QueriesResource : IQueriesResource
    {
        public const string CompanyQuery = @"
            SELECT
                Id AS Id,
                Name AS Name,
                RegisterNumber AS RegisterNumber,
                EstablishedOn AS EstablishedOn
            FROM 
                Companies AS Company
            WHERE
                (@Id IS NULL OR Id = @Id)
        ";

        public const string EmployeeDetailed = @"
            SELECT 
                e.Id as Id,
                e.Name as Name,
                e.Email as Email,
                e.Birth as Birth,
                e.CompanyId as CompanyId,
                c.Name as CompanyName
            FROM 
                Employees e
            INNER JOIN 
                Companies c
                    ON e.CompanyId = c.Id
            WHERE 
                (@Id IS NULL OR e.Id = @Id)
        ";
    }
}
