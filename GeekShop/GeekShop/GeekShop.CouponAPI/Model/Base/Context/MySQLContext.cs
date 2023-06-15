using Microsoft.EntityFrameworkCore;

namespace GeekShop.CouponAPI.Model.Base.Context;

public class MySQLContext : DbContext
{
    public MySQLContext()
    {
    }

    public MySQLContext(DbContextOptions<MySQLContext> opt) : base(opt)
    {
    }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            Id = 1,
            CouponCode = "NICAO_10",
            Discount = 10
        });

        modelBuilder.Entity<Coupon>().HasData(new Coupon
        {
            Id = 2,
            CouponCode = "NICAO_20",
            Discount = 20
        });
    }
}