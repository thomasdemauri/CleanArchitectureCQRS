using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Exceptions.ContractExceptions
{
    internal class AdmissionDateCannotBeInFutureException : BaseDomainException
    {
        public AdmissionDateCannotBeInFutureException() : base("Admission date cannot be in the future.")
        {
        }
    }
}
