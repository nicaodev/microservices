using GeekShop.CartAPI.Model.Repository;
using GeekShop.OrderAPI.Messages;
using GeekShop.OrderAPI.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShop.OrderAPI.MessageConsumer;

public class RabbitMQCheckoutConsumer : BackgroundService
{
    private readonly OrderRepository _orderRepository;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQCheckoutConsumer(OrderRepository orderRepository)
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
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (canal, evento) =>
        {
            var content = Encoding.UTF8.GetString(evento.Body.ToArray());
            CheckoutHeaderDto dto = JsonSerializer.Deserialize<CheckoutHeaderDto>(content);
            ProcessOrder(dto).GetAwaiter().GetResult();
            _channel.BasicAck(evento.DeliveryTag, false); // remove msg da lista do Manager RABBITMQ.
        };
        _channel.BasicConsume("checkoutqueue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task ProcessOrder(CheckoutHeaderDto dto)
    {
        OrderHeader order = new()
        {
            UserId = dto.UserId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            OrderDetails = new List<OrderDetail>(),
            CardNumber = dto.CardNumber,
            CouponCode = dto.CouponCode,
            CVV = dto.CVV,
            DiscountAmount = dto.DiscountAmount,
            Email = dto.Email,
            ExpiryMothYear = dto.ExpiryMothYear,
            OrderTime = DateTime.Now,
            PaymentStatus = false,
            Phone = dto.Phone,
            DateTime = dto.DateTime,
        };

        foreach (var details in dto.CartDetails)
        {
            OrderDetail detail = new()
            {
                ProductId = details.ProductId,
                ProductName = details.Product.Name,
                Price = details.Product.Price,
                Count = details.Count,
            };

            order.CartTotalItens += details.Count;
            order.OrderDetails.Add(detail);
        }

        await _orderRepository.AddOrder(order);
    }
}