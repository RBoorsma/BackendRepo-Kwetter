using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public interface IRabbitMQPublisher
{
    bool SendMessage(IPublishData publishData);
    bool NewConnection();
    public IBasicProperties GetProperties();
}