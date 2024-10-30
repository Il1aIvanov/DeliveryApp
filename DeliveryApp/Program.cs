using DeliveryApp.Formatters;
using DeliveryApp.Services.Interfaces;
using DeliveryApp.Repositories.Interfaces;
using DeliveryApp.Services;
using DeliveryApp.Repositories;
using DeliveryApp.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Регистрация зависимостей
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<FileWriter>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Добавляем контроллеры и Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new DateTimeJsonFormatter());
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();