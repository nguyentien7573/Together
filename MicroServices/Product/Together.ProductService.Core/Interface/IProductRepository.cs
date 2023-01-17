
using Together.Core.Domain;
using Together.Core.Repository;
using Together.ProductService.Core.Entities;

namespace Together.ProductService.Core.Interface
{
    public interface IProductRepository<TEntity>  : IRepository<Product> where TEntity : EntityBase
    {
        Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId);
    }
}
