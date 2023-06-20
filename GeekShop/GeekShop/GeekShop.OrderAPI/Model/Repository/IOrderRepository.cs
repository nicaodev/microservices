using GeekShop.OrderAPI.Model;

namespace GeekShop.CartAPI.Model.Repository;

public interface IOrderRepository
{
    Task<bool> AddOrder(OrderHeader header);

    Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid);
}