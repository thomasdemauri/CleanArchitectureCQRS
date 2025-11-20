using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Commands.Employee
{
    public class CreateEmployeeCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required DateTime Birth { get; set; }
        public required Guid CompanyId { get; set; }

        public CreateEmployeeCommand(string name, string email, DateTime birth, Guid companyId)
        {
            Name = name;
            Email = email;
            Birth = birth;
            CompanyId = companyId;
        }
    }
}
