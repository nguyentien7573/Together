using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.Infrastructure.Data;
using Together.Shopping.Core.Entities;
using Together.Shopping.Infrastructor.Data;

namespace Together.Shopping.Infrastructor.Repositories
{
    public class ShoppingSessionRepository : RepositoryBase<ShoppingtDbContext, ShoppingSession>
    {
        public ShoppingSessionRepository(ShoppingtDbContext dbContext) : base(dbContext)
        {
        }
    }
}
