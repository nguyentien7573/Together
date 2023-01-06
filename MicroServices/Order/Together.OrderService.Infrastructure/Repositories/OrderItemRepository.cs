using Together.Infrastructure.Data;
using Together.OrderService.Core.Entities;
using Together.OrderService.Infrastructure.Data;

namespace Together.OrderService.Infrastructure.Repositories
{
    public class OrderItemRepository : RepositoryBase<OrderDbContext, OrderItem>
    {
        public OrderItemRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
