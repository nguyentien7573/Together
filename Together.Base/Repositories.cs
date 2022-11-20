using Microsoft.EntityFrameworkCore;
using Together.Base.Entities;
using Together.Base.Interface;

namespace Together.Base
{
    public class Repository<Context, T> : IRepository<T> where Context : DbContext
                                                         where T : Entity
    {
        protected readonly DbContext ct;

        public Repository(Context context)
        {
            ct = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await ct.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await ct.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await ct.Set<T>().AddAsync(entity);
            await ct.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            ct.Entry(entity).State = EntityState.Modified;
            entity.LastUpdate = DateTime.UtcNow;
            await ct.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            ct.Set<T>().Remove(entity);
            await ct.SaveChangesAsync();
        }
    }
}
