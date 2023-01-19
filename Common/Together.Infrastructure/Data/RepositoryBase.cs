using Together.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Together.Core.Domain;

namespace Together.Infrastructure.Data
{
    public class RepositoryBase<TDbContext, TEntity> : IRepository<TEntity>
        where TEntity : EntityBase
        where TDbContext : DbContext
    {
        public readonly TDbContext _dbContext;

        protected RepositoryBase(TDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<TEntity> FindById(Guid id)
        {
            var temp = await _dbContext.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
            return temp;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            var tempEntity = _dbContext.Set<TEntity>().SingleOrDefault(e => e.Id == entity.Id);

            if (tempEntity != null)
            {
                tempEntity.IsActive = false;
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            var tempEntity = _dbContext.Set<TEntity>().SingleOrDefault(e => e.Id == entity.Id);

            if (tempEntity != null)
            {
                tempEntity = entity;
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            else
            {
                return false;
            }
        }
    }
}
