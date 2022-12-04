using Together.Infrastructure.Data;
using Together.CustomerService.Infrastructor.Data;
using Together.CustomerService.Core.Entities;

namespace Together.CustomerService.Infrastructor.Repositories
{
    public class CustomerRepository : RepositoryBase<CustomerDbContext, Customer>
    {
        public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
        {
        }
    }
}
