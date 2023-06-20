using Microsoft.EntityFrameworkCore;

namespace GeekShop.OrderAPI.Model;

public class MySQLContext : DbContext
{
    public MySQLContext()
    {
    }

    public MySQLContext(DbContextOptions<MySQLContext> opt) : base(opt)
    {
    }

    public DbSet<OrderDetail> Details { get; set; }
    public DbSet<OrderHeader> Headers { get; set; }
}