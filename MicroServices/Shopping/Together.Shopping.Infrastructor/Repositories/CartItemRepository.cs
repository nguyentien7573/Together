using Together.Infrastructure.Data;
using Together.Shopping.Core.Entities;
using Together.Shopping.Infrastructor.Data;

namespace Together.Shopping.Infrastructor.Repositories
{
    public class CartItemRepository : RepositoryBase<ShoppingtDbContext, CartItem>
    {
        public CartItemRepository(ShoppingtDbContext dbContext) : base(dbContext)
        {
        }
    }
}
