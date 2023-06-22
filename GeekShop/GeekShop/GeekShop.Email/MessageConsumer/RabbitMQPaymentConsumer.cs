using GeekShop.Email.Messages;
using GeekShop.Email.Model.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShop.Email.MessageConsumer;

public class RabbitMQPaymentConsumer : BackgroundService
{
    private readonly EmailRepository _emailRepository;
    private IConnection _connection;
    private IModel _channel;
    private const string ExchangeName = "FanoutPaymentUpdateExchange";
    private string queueName = "";

    public RabbitMQPaymentConsumer(EmailRepository emailRepository)
    {
        _emailRepository = emailRepository;

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
            UpdatePaymentResultMessage message = JsonSerializer.Deserialize<UpdatePaymentResultMessage>(content) ?? new UpdatePaymentResultMessage();
            ProcessLogs(message).GetAwaiter().GetResult();
            _channel.BasicAck(evento.DeliveryTag, false); // remove msg da lista do Manager RABBITMQ.
        };
        _channel.BasicConsume(queueName, false, consumer);

        return Task.CompletedTask;
    }

    private async Task ProcessLogs(UpdatePaymentResultMessage message)
    {
        try
        {
            await _emailRepository.LogEmail(message);
        }
        catch (Exception)
        {
            //Log exception
            throw;
        }
    }
}