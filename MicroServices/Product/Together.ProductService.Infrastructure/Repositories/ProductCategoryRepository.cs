using Together.Infrastructure.Data;
using Together.ProductService.Core.Entities;
using Together.ProductService.Infrastructure.Data;

namespace Together.ProductService.Infrastructure.Repositories
{
    public class ProductCategoryRepository : RepositoryBase<ProductDbContext, Category>
    {
        public ProductCategoryRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }
    }
}
