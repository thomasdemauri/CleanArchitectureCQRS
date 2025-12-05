using CleanArchitecture.Domain.Core.Data;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DbSet<TEntity> _entity;
        protected readonly AppDbContext _context;
        public virtual IUnitOfWork UnitOfWork => _context;
        public Repository(AppDbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public Task<TEntity> Add(TEntity entity)
        {
            _entity.AddAsync(entity);
            return Task.FromResult(entity);
        }

        public void Delete(TEntity entity)
        {
            _entity.Remove(entity);
        }

        public Task<TEntity> Update(TEntity entity)
        {
            var entry = _context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                _entity.Attach(entity);
                entry.State = EntityState.Modified;
            }

            return Task.FromResult(entity);
        }

        public virtual async Task<TEntity?> GetById(TKey id)
        {
            return await _entity.FindAsync(id);
        }

    }
}
