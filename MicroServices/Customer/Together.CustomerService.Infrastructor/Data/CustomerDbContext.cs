using Microsoft.EntityFrameworkCore;
using Together.CustomerService.Core.Entities;
using Together.Infrastructure.Data;

namespace Together.CustomerService.Infrastructor.Data
{
    public class CustomerDbContext : AppDbContextBase
    {
        private const string Schema = "customer";

        public CustomerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
