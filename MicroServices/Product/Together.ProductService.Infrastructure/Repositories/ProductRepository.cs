using Together.ProductService.Core.Entities;
using Together.Infrastructure.Data;
using Together.ProductService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Together.ProductService.Core.Interface;

namespace Together.ProductService.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<ProductDbContext, Product>, IProductRepository<Product>
    {
        public ProductRepository(ProductDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId)
        {
            var products = await _dbContext.Products.Where(x => x.CategoryId == categoryId).ToListAsync();

            return products;
        }
    }
}
