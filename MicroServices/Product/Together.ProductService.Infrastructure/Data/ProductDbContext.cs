using Microsoft.EntityFrameworkCore;
using Together.Infrastructure.Data;
using Together.ProductService.Core.Entities;

namespace Together.ProductService.Infrastructure.Data
{
    //Command : add-migration Together.Product
    public class ProductDbContext : AppDbContextBase
    {
        private const string Schema = "prod";
        public ProductDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().ToTable("Products", Schema);
            //modelBuilder.Entity<Product>().HasKey(x => x.Id);
            //modelBuilder.Entity<Product>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            //modelBuilder.Entity<Category>().ToTable("Categories", Schema);
            //modelBuilder.Entity<Category>().HasKey(x => x.Id);
            //modelBuilder.Entity<Category>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
