using GeekShop.CartAPI.Model.Repository;
using GeekShop.OrderAPI.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShop.OrderAPI.MessageConsumer;

public class RabbitMQPaymentConsumer : BackgroundService
{
    private readonly OrderRepository _orderRepository;
    private IConnection _connection;
    private IModel _channel;
    private const string ExchangeName = "FanoutPaymentUpdateExchange";
    private string queueName = "";

    public RabbitMQPaymentConsumer(OrderRepository orderRepository)
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

        //_channel.QueueDeclare(queue: "orderpaymentprocessQueue", false, false, false, arguments: null);

        _channel.ExchangeDeclare(exchange: ExchangeName, ExchangeType.Fanout);
        queueName = _channel.QueueDeclare(queueName).QueueName;
        _channel.QueueBind(queueName, ExchangeName, "");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (canal, evento) =>
        {
            var content = Encoding.UTF8.GetString(evento.Body.ToArray());
            UpdatePaymentResultDto dto = JsonSerializer.Deserialize<UpdatePaymentResultDto>(content);
            UpdatePaymentStatus(dto).GetAwaiter().GetResult();
            _channel.BasicAck(evento.DeliveryTag, false); // remove msg da lista do Manager RABBITMQ.
        };
        _channel.BasicConsume(queueName, false, consumer);

        return Task.CompletedTask;
    }

    private async Task UpdatePaymentStatus(UpdatePaymentResultDto dto)
    {
        try
        {
            await _orderRepository.UpdateOrderPaymentStatus(dto.OrderId, dto.Status);
        }
        catch (Exception)
        {
            //Log exception
            throw;
        }
    }
}