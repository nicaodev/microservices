using GeekShop.MessageBus;

namespace GeekShop.OrderAPI.RabbitMQSender;

public interface IRabbitMQSender
{
    void SendMessage(BaseMessage baseMessage, string queueName);
}