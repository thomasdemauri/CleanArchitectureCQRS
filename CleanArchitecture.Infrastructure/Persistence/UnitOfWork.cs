using CleanArchitecture.Application;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
