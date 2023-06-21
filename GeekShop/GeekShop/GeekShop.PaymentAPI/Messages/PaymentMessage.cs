using GeekShop.MessageBus;

namespace GeekShop.PaymentAPI.Messages;

public class PaymentDto : BaseMessage
{
    public long OrderId { get; set; }
    public string Name { get; set; }

    public string CardNumber { get; set; }

    public string CVV { get; set; }
    public string ExpiryMonthYear { get; set; }

    public decimal PuchaseAmount { get; set; }
    public string Email { get; set; }
}