using GeekShop.CartAPI.Model.Repository;
using RabbitMQ.Client;

namespace GeekShop.OrderAPI.MessageConsumer;

public class RabbitMQMessageConsumer : BackgroundService
{
    private readonly OrderRepository _orderRepository;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQMessageConsumer(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Password = "guest",
            UserName = "guest",
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}