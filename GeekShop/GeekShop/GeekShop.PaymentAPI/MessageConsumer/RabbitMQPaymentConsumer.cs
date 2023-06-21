using GeekShop.PaymentAPI.Messages;
using GeekShop.PaymentAPI.RabbitMQSender;
using GeekShop.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShop.PaymentAPI.MessageConsumer;

public class RabbitMQPaymentConsumer : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private IRabbitMQSender _rabbitMQSender;
    private readonly IProcessPayment _processPayment;

    public RabbitMQPaymentConsumer(IProcessPayment processPayment, IRabbitMQSender rabbitMQSender)
    {
        _rabbitMQSender = rabbitMQSender;
        _processPayment = processPayment;

        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            Password = "guest",
            UserName = "guest",
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "orderpaymentprocessQueue", false, false, false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (canal, evento) =>
        {
            var content = Encoding.UTF8.GetString(evento.Body.ToArray());
            PaymentMessage dto = JsonSerializer.Deserialize<PaymentMessage>(content);
            ProcessPayment(dto).GetAwaiter().GetResult();
            _channel.BasicAck(evento.DeliveryTag, false); // remove msg da lista do Manager RABBITMQ.
        };
        _channel.BasicConsume("orderpaymentprocessQueue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task ProcessPayment(PaymentMessage dto)
    {

        var result = _processPayment.PaymentProcessor(); // Somente um mock.

        UpdatePaymentResultMessage paymentResult = new UpdatePaymentResultMessage()
        {
            Status = result,
            OrderId = dto.OrderId,
            Email = dto.Email
        };


        try
        {
            _rabbitMQSender.SendMessage(paymentResult);
        }
        catch (Exception)
        {
            //Log exception
            throw;
        }
    }
}