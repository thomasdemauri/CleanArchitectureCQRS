using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {

        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public Task<TEntity> Add(TEntity entity)
        {
            _context.Set<TEntity>().AddAsync(entity);
            return Task.FromResult(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public Task<TEntity> Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return Task.FromResult(entity);
        }

    }
}
