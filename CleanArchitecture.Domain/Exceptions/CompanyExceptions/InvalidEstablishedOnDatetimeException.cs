using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Exceptions.CompanyExceptions
{
    internal class InvalidEstablishedOnDatetimeException : BaseDomainException
    {
        public InvalidEstablishedOnDatetimeException() : base("Date cannot be in future.")
        {
        }
    }
}
