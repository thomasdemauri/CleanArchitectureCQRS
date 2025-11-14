using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Exceptions
{
    internal class BaseDomainException : Exception
    {
        public BaseDomainException(string message) : base(message) { }
    }
}
