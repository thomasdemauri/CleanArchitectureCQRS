using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Shared;

public class Employee : AgreggateRoot<Guid>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime Birth { get; private set; }
    public Guid CompanyId { get; private set; }
    public Company Company { get; private set; }

    protected Employee() { }

    private Employee(Guid id, string name, string email, DateTime birth, Guid companyId)
    {
        Name = name;
        Email = email;
        Birth = birth;
        CompanyId = companyId;
    }

    public Employee Create(string name, string email, DateTime birth, Guid companyId)
    {
        return new Employee(Guid.NewGuid(), name, email, birth, companyId);
    }
}
