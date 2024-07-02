using Messaging.RabbitMQ;
using RegisterOrchService.Core.Messaging;
using RegisterOrchService.Core.Profiles;
using RegisterOrchService.Core.Services;
using RegisterOrchService.Core.Services.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRegisterOrchService, RegisterOrchServiceCore>();
builder.Services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>();
builder.Services.AddTransient(typeof(IRabbitMQReceiver<>), typeof(RabbitMQReceiver<>));
builder.Services.AddTransient<IMessageHandler, MessageHandler>();
builder.Services.AddSingleton<IMQConnection, MQConnection>();
builder.Services.AddAutoMapper(typeof(OrchestrationProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RegisterOrch Service");
    });
}
// Cors
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();