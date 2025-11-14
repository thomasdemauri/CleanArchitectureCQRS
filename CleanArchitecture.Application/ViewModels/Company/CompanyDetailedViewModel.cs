using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ViewModels.Company
{
    public class CompanyDetailedViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime EstablishedOn { get; set; }
    }
}
