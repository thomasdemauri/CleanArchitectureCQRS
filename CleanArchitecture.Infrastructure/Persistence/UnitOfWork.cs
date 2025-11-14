using CleanArchitecture.Application;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
    }
}
