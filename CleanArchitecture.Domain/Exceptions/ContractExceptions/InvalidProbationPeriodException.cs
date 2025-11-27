namespace CleanArchitecture.Domain.Exceptions.ContractExceptions
{
    public class InvalidProbationPeriodException : BaseDomainException
    {
        public InvalidProbationPeriodException(string message) : base("Probation period must have 90 days")
        {
        }
    }
}