using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public interface IRabbitMQPublisher
{
    public bool DeclareQueues(string queue, bool durable = true, bool exclusive = false, bool autoDelete = false);
    bool SendMessage(IPublishData publishData);
    bool NewConnection();
    public IBasicProperties GetProperties();
}