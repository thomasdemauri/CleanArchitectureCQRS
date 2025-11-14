using CleanArchitecture.Domain.Exceptions.ValueObjectsExceptions.BirthDateExceptions;

namespace CleanArchitecture.Domain.ValueObjects
{
    public class BirthDate
    {
        public DateTime Birth { get; init; }

        internal BirthDate(DateTime birth)
        {
            Birth = birth;
        }

        public static BirthDate Create(DateTime birth)
        {
            VerifyDate(birth);

            return new BirthDate(birth);
        }
        
        private static bool VerifyDate(DateTime birth)
        {
            if (birth >= DateTime.Now)
            {
                throw new DateBirthEqualOrGreaterThanNowException();
            }

            return true;
        }
    }
}
