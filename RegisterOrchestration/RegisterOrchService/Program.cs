using Messaging.RabbitMQ;
using RegisterOrchService.Core.Messaging;
using RegisterOrchService.Core.Profiles;
using RegisterOrchService.Core.Services;
using RegisterOrchService.Core.Services.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRegisterOrchService, RegisterOrchServiceCore>();
builder.Services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>();
builder.Services.AddTransient(typeof(IRabbitMQReceiver<>), typeof(RabbitMQReceiver<>));
builder.Services.AddTransient<IMessageHandler, MessageHandler>();
builder.Services.AddTransient<IMQConnection, MQConnection>();
builder.Services.AddAutoMapper(typeof(OrchestrationProfile));
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