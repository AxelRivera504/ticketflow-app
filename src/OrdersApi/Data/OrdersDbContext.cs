using OrdersApi.Models;
using Microsoft.EntityFrameworkCore;

namespace OrdersApi.Data;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .Property(o => o.UnitPrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Order>()
            .Ignore(o => o.TotalPrice);
    }
}
