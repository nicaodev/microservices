using Microsoft.EntityFrameworkCore;

namespace GeekShop.CartAPI.Model.Base.Context;

public class MySQLContext : DbContext
{
    public MySQLContext()
    {
    }

    public MySQLContext(DbContextOptions<MySQLContext> opt) : base(opt)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }
}