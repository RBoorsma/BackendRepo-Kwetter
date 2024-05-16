using RabbitMQ.Client;
using UserService.Core.Services.Events;


namespace UserService.Core.Messaging.RabbitMQ;
using RabbitMQ;
public interface IRabbitMqConsumer
{
    void Receive();
    event EventHandler<MessageReceivedEventArgs> MessageReceived;
}