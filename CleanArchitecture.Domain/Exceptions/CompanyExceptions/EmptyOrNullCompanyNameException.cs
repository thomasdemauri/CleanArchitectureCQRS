namespace CleanArchitecture.Domain.Exceptions.CompanyExceptions
{
    internal class EmptyOrNullCompanyNameException : BaseDomainException
    {
        public EmptyOrNullCompanyNameException() : base("Company name cannot be null or empty.")
        {
        }
    }
}
