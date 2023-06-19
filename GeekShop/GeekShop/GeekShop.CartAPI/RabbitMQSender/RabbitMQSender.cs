using GeekShop.CartAPI.Messages;
using GeekShop.MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShop.CartAPI.RabbitMQSender;

public class RabbitMQSender : IRabbitMQSender
{
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _userName;
    private IConnection _connection;

    public RabbitMQSender()
    {
        _hostName = "localhost";
        _password = "guest";
        _userName = "guest";
    }

    public void SendMessage(BaseMessage message, string queueName)
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostName,
            Password = _password,
            UserName = _userName,
        };

        _connection = factory.CreateConnection();
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);

        byte[] body = GetMessageAsByteArray(message);
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }

    private byte[] GetMessageAsByteArray(object message)
    {
        var opt = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize<CheckoutHeaderDto>((CheckoutHeaderDto)message, opt);

        var body = Encoding.UTF8.GetBytes(json);
        return body;
    }
}