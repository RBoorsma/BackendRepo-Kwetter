using RabbitMQ.Client;
using UserProfileService.Core.Messaging.Events;
using UserProfileService.Core.Service;


namespace UserProfileService.Core.Messaging.RabbitMQ;
using RabbitMQ;
public interface IRabbitMqConsumer
{
    void Receive();
    event EventHandler<MessageReceivedEventArgs> MessageReceived;
}