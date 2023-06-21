using GeekShop.CartAPI.Model.Repository;
using GeekShop.OrderAPI.MessageConsumer;
using GeekShop.OrderAPI.Model;
using GeekShop.OrderAPI.RabbitMQSender;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MySQLContext>(opt => opt.UseSqlServer(connection));

builder.Services.AddDbContext<MySQLContext>(opt => opt.UseSqlServer(connection));

var DbContextOptionsBuilder = new DbContextOptionsBuilder<MySQLContext>();
DbContextOptionsBuilder.UseSqlServer(connection);

builder.Services.AddSingleton(new OrderRepository(DbContextOptionsBuilder.Options));

builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();
builder.Services.AddHostedService<RabbitMQPaymentConsumer>();
builder.Services.AddSingleton<IRabbitMQSender, RabbitMQSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();