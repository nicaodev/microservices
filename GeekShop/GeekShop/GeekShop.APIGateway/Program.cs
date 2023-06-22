using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot();


//app.UseAuthorization();

//app.MapControllers();

app.Run();