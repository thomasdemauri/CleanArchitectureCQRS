namespace CleanArchitecture.Domain.Exceptions.CompanyExceptions
{
    internal class InvalidRegisterNumberException : BaseDomainException
    {
        public InvalidRegisterNumberException() : base("Invalid register number. Only 14 digits.")
        {
        }
    }
}
