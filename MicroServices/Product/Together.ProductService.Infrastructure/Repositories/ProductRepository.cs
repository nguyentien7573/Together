using Together.ProductService.Core.Entities;
using Together.Infrastructure.Data;
using Together.ProductService.Infrastructure.Data;

namespace Together.ProductService.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<ProductDbContext, Product>
    {
        public ProductRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }
    }
}
