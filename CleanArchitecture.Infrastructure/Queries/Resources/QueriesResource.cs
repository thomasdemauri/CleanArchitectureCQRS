using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Queries.Resources
{
    public class QueriesResource
    {
        public static string CompanyQuery => @"
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

    }
}
