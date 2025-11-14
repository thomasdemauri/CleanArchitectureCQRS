namespace CleanArchitecture.Domain.Exceptions.ValueObjectsExceptions.BirthDateExceptions
{
    internal class DateBirthEqualOrGreaterThanNowException : BaseDomainException
    {
        public DateBirthEqualOrGreaterThanNowException() 
            : base("Date birth cannot be equal or greater than now.")
        {
        }
    }
}
