using Microsoft.EntityFrameworkCore;
using Together.Infrastructure.Data;
using Together.OrderService.Core.Entities;
using Together.OrderService.Core.Entities.Address;

namespace Together.OrderService.Infrastructure.Data
{
    public class OrderDbContext : AppDbContextBase
    {
        private string Schema = "ord";
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<AdministrativeRegion> AdministrativeRegions { get;set;}
        public DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Ward> Wards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders", Schema);
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<OrderItem>().ToTable("OrderItems", Schema);
            modelBuilder.Entity<OrderItem>().HasKey(x => x.Id);
            modelBuilder.Entity<OrderItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<AdministrativeRegion>().HasKey(x => x.Id);
            modelBuilder.Entity<AdministrativeUnit>().HasKey(x => x.Id);
            modelBuilder.Entity<District>().HasKey(x => x.Code);
            modelBuilder.Entity<Province>().HasKey(x => x.Code);
            modelBuilder.Entity<Ward>().HasKey(x => x.Code);
        }
    }
}
