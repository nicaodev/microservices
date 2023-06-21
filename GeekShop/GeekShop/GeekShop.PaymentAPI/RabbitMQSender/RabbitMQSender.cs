using GeekShop.MessageBus;
using GeekShop.PaymentAPI.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShop.PaymentAPI.RabbitMQSender;

public class RabbitMQSender : IRabbitMQSender
{
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _userName;
    private IConnection _connection;

    private const string ExchangeName = "FanoutPaymentUpdateExchange";

    public RabbitMQSender()
    {
        _hostName = "localhost";
        _password = "guest";
        _userName = "guest";
    }

    public void SendMessage(BaseMessage message)
    {
        if (ConnectionExists())
        {
            using var channel = _connection.CreateModel();

            //channel.QueueDeclare(queue: queueName, false, false, false, arguments: null); // Fila simples RabbitMQ
            //channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body); // Fila simples RabbitMQ

            channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout, durable: false); 
            byte[] body = GetMessageAsByteArray(message);
            channel.BasicPublish(exchange: ExchangeName, "", basicProperties: null, body: body);
        }
    }

    private byte[] GetMessageAsByteArray(object message)
    {
        var opt = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize<UpdatePaymentResultMessage>((UpdatePaymentResultMessage)message, opt);

        var body = Encoding.UTF8.GetBytes(json);
        return body;
    }

    private bool ConnectionExists()
    {
        if (_connection is not null) return true;

        CreateConnection();

        return _connection != null;
    }

    private void CreateConnection()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                Password = _password,
                UserName = _userName,
            };

            _connection = factory.CreateConnection();
        }
        catch (Exception)
        {
            //Log exception
            throw;
        }
    }
}