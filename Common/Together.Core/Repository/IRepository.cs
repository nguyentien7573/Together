using Together.Core.Domain;

namespace Together.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity FindById(Guid id);
        Task<TEntity> FindOneAsync(TEntity entity);
        Task<List<TEntity>> FindAsync(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task Update(TEntity entity);
    }
}
