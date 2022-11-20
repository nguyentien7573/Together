using Microsoft.EntityFrameworkCore;
using Together.Products.Core.Entities;

namespace Together.Products.Infrastructure.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=NGUYENTIEN;Database=Together.Product;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<Product> Products { get; set; }
    }
}
