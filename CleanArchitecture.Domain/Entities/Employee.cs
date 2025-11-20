using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Entities
{
    public class Employee : AgreggateRoot<Guid>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime Birth { get; private set; }
        public Guid CompanyId { get; private set; }
        public Company Company { get; private set; }

        private Employee(Guid id, string name, string email, DateTime birth, Guid companyId)
        {
            Name = name;
            Email = email;
            Birth = birth;
            CompanyId = companyId;
        }

        public static Employee Create(string name, string email, DateTime birth, Guid companyId)
        {
            if (birth > DateTime.Now)
            {
                throw new Exception("Date birth cannot be in future"); // mudar aqui tambem depois
            }

            return new Employee(Guid.NewGuid(), name, email, birth, companyId);
        }
    }

}
