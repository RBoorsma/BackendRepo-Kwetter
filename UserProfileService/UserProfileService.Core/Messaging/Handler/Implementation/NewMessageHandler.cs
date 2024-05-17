using Kwetter.Library.Messaging.Enums;
using Messaging;
using Messaging.RabbitMQ;
using UserProfileService.Core.Messaging.Events;
using UserProfileService.Core.Messaging.Models;

namespace UserProfileService.Core.Messaging.Handler;

public class NewMessageHandler 
{

    private MQConnection Connection;
    private RabbitMQReceiver<UserRequestBody> receiver;

    private PublishDataBuilder<UserRequestBody> MessageBuilder = new PublishDataBuilder<UserRequestBody>();
    private RabbitMQPublisher publisher;

    public NewMessageHandler()
    {
        Connection = new MQConnection();
        receiver = new RabbitMQReceiver<UserRequestBody>(Connection);
        receiver.MessageReceived += OnMessageReceived;
        publisher = new RabbitMQPublisher(Connection);
    }
    public void SendStatus(UserRequestBody body)
    {
       IPublishData<UserRequestBody> data = MessageBuilder.setBody(body).setRoutingKey(RoutingKey.RegisterQueue).build();
       publisher.SendProfileStatus<UserRequestBody>(data);
    }

    public void OnMessageReceived(object? sender, MessageReceivedEventArgs<UserRequestBody> e)
    {
       Console.WriteLine(e.Data.UserID);
    }

    public void StartListening()
    {
        receiver.StartListeningTo(MessageQueue.RegisterQueue);
    }
}