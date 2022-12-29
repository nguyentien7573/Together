using Together.Infrastructure.Data;
using Together.ProductService.Core.Entities;
using Together.ProductService.Infrastructure.Data;

namespace Together.ProductService.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<ProductDbContext, Category>
    {
        public CategoryRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }
    }
}
