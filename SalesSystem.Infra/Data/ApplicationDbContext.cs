
using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Entities;

namespace SalesSystem.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrdemItems");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.ToTable("OrderStatuses");
        });
    }
}
