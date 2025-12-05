namespace CleanArchitecture.Domain.Core.Data
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        void Delete(TEntity entity);
        public Task<TEntity?> GetById(TKey id);
        IUnitOfWork UnitOfWork { get; }
    }
}