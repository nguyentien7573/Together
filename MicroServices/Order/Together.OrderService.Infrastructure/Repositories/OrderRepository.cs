﻿using Together.Infrastructure.Data;
using Together.OrderService.Core.Entities;
using Together.OrderService.Infrastructure.Data;

namespace Together.OrderService.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<OrderDbContext, Order>
    {
        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
