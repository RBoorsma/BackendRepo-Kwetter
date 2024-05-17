using Kwetter.Library.Messaging.Enums;

namespace Messaging;

public class PublishData<T> : IPublishData<T>
{
    public Exchanges Exchange { get; }
    public RoutingKey RoutingKey { get; }
    public T data { get; }
    public QueueOptions QueueOptions { get;  }
    public Guid CorreletionID { get; }

    internal PublishData(T data, RoutingKey RoutingKey, Exchanges exchange, QueueOptions queueOptions,
        Guid correlationId)
    {
        this.data = data;
        this.RoutingKey = RoutingKey;
        this.QueueOptions = queueOptions;
        this.Exchange = exchange;
        this.CorreletionID = correlationId;
    }
}