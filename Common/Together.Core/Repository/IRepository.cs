using Together.Core.Domain;

namespace Together.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> FindById(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> RemoveAsync(TEntity entity);
        Task<bool> Update(TEntity entity);
    }
}
