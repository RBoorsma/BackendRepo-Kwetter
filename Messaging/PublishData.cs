using Kwetter.Library.Messaging.Enums;

namespace Messaging;

public class PublishData<T> : IPublishData<T>
{
    public RoutingKey QueueName { get; }
    public T data { get; }
    public bool Durable { get; } = true;
    public bool Exclusive { get; } = false;
    public bool Persistent { get; } = true;
    public Guid CorreletionID { get; } = Guid.NewGuid();

    internal PublishData(T data, RoutingKey queueName, bool durable, bool exclusive, bool persistent,
        Guid correlationId)
    {
        this.data = data;
    }
}