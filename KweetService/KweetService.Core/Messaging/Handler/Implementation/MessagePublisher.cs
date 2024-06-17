using Kwetter.Library.Messaging.Datatypes;
using Messaging.RabbitMQ;
using Messaging.RabbitMQ.HandlerInterface;

namespace KweetService.Core.Messaging.Handler;

public class MessagePublisher(IRabbitMQPublisher publisher) : IPublishHandler
{
    private PublishDataBuilder builder = new();
    public bool SendStatus<T>(T body)
    {
        IPublishData data =  builder.setRoutingKey(RoutingKey.Registration).setBody(new MessageContainer<T>(body)).build();
        publisher.SendMessage(data);
        return true;

    }

  
    
}