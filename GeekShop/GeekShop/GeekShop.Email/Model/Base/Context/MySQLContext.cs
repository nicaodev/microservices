using Microsoft.EntityFrameworkCore;

namespace GeekShop.Email.Model.Base.Context;

public class MySQLContext : DbContext
{
    public MySQLContext()
    {
    }

    public MySQLContext(DbContextOptions<MySQLContext> opt) : base(opt)
    {
    }

    public DbSet<EmailLog> Emails { get; set; }
}