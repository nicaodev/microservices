using GeekShop.MessageBus;

namespace GeekShop.PaymentAPI.RabbitMQSender;

public interface IRabbitMQSender
{
    void SendMessage(BaseMessage baseMessage);
}