using GeekShop.MessageBus;

namespace GeekShop.CartAPI.RabbitMQSender;

public interface IRabbitMQSender
{
    void SendMessage(BaseMessage baseMessage, string queueName);
}