namespace GeekShop.Email.Model.Repository;

public interface IOrderRepository
{
    Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid);
}