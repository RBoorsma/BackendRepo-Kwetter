using KweetService.Core.Messaging.Handler;
using KweetService.Core.Service;
using KweetService.DAL.Repository;
using Kwetter.Library.Messaging.Datatypes;
using Messaging.RabbitMQ;
using Messaging.RabbitMQ.HandlerInterface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IKweetRepository, KweetRepository>();
builder.Services.AddTransient<IKweetService, KweetServiceCore>();
builder.Services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>();
builder.Services.AddTransient(typeof(IRabbitMQReceiver<>), typeof(RabbitMQReceiver<>));
builder.Services.AddTransient<IReceiverHandler<DefaultMessageData>, ProfileMessageReceiver>();
builder.Services.AddTransient<IPublishHandler, MessagePublisher>();
builder.Services.AddSingleton<IMQConnection, MQConnection>();
builder.Services.AddTransient<IKweetProfilesService, KweetProfilesService>();
builder.Services.AddTransient<IProfileRepository, ProfileRepository>();

var configuration = builder.Configuration;
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kweet Service");
    });
}
// Cors
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

IReceiverHandler<DefaultMessageData> handler =  app.Services.GetRequiredService<IReceiverHandler<DefaultMessageData>>();
handler.StartListening();
app.UseAuthorization();
app.MapControllers();


app.Run();