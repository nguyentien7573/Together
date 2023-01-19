using Microsoft.EntityFrameworkCore;
using Together.Infrastructure.Data;
using Together.Shopping.Core.Entities;

namespace Together.Shopping.Infrastructor.Data
{
    public class ShoppingtDbContext : AppDbContextBase
    {
        private const string Schema = "shop";
        public ShoppingtDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ShoppingSession> ShoppingSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().ToTable("Products", Schema);
            modelBuilder.Entity<CartItem>().HasKey(x => x.Id);
            modelBuilder.Entity<CartItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<ShoppingSession>().ToTable("Categories", Schema);
            modelBuilder.Entity<ShoppingSession>().HasKey(x => x.Id);
            modelBuilder.Entity<ShoppingSession>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
