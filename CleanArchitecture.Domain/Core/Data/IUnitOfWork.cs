namespace CleanArchitecture.Domain.Core.Data;

public interface IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);

}
