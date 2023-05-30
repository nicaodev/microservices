using Microsoft.EntityFrameworkCore;

namespace GeekShop.ProductAPI.Model.Base.Context;

public class MySQLContext : DbContext
{
    public MySQLContext()
    {
    }

    public MySQLContext(DbContextOptions<MySQLContext> opt) : base(opt)
    {
    }

    public DbSet<Product> Products { get; }
}