using GeekShop.Email.Model.Base.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShop.Email.Model.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly DbContextOptions<MySQLContext> _context;

    public OrderRepository(DbContextOptions<MySQLContext> context)
    {
        _context = context;
    }

    public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
    {
        //await using var _db = new MySQLContext(_context);
        //var header = await _db.Headers.FirstOrDefaultAsync(x => x.Id == orderHeaderId);

        //if (header is not null)
        //{
        //    header.PaymentStatus = status;
        //    await _db.SaveChangesAsync();
        //}
    }
}