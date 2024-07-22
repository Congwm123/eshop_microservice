using DisCount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace DisCount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "Iphone X", Description = "Iphone X", Amount = 150 },
            new Coupon { Id = 2, ProductName = "Samsung S23", Description = "Samsung S23", Amount = 100 });
    }
}
