using Together.Base;
using Together.Products.Core.Entities;
using Together.Products.Core.Repositories;
using Together.Products.Infrastructure.Data;

namespace Together.Products.Infrastructure.Repositories
{
    public class ProductRepository : Repository<ProductContext, Product>, IProductRepository
    {
        public ProductRepository(ProductContext context) : base(context)
        {
        }
    }
}
