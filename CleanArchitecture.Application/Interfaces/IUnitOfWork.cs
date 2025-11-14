namespace CleanArchitecture.Application;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
