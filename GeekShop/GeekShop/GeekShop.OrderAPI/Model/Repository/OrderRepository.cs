using GeekShop.OrderAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShop.CartAPI.Model.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly DbContextOptions<MySQLContext> _context;

    public OrderRepository(DbContextOptions<MySQLContext> context)
    {
        _context = context;
    }

    public async Task<bool> AddOrder(OrderHeader header)
    {
        if (header is null) return false;

        await using var _db = new MySQLContext(_context);

        _db.Headers.Add(header);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
    {
        await using var _db = new MySQLContext(_context);
        var header = await _db.Headers.FirstOrDefaultAsync(x => x.Id == orderHeaderId);

        if (header is not null)
        {
            header.PaymentStatus = status;
            await _db.SaveChangesAsync();
        }
    }
}