
using Kwetter.Library.Messaging.Datatypes;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public class PublishData : IPublishData
{
    public IMessage data { get; }
    public Exchanges Exchange { get; }
    public string RoutingKey { get; }
    //public T data { get; }
    public QueueOptions? QueueOptions { get;  }
    public Guid CorreletionID { get; }

    public IBasicProperties? Properties { get;  }

    public T GetData<T>()
    {
        if (data is MessageContainer<T> actualData)
        {
            return actualData.Data;
        }

        throw new InvalidCastException("The Publisher hasn't received the same class that is used in the PublishData!");
    }

    internal PublishData(IMessage data, string RoutingKey, Exchanges exchange, QueueOptions queueOptions,
        Guid correlationId, IBasicProperties properties)
    {
        this.data = data;
        this.RoutingKey = RoutingKey;
        this.QueueOptions = queueOptions;
        this.Exchange = exchange;
        this.CorreletionID = correlationId;
        this.Properties = properties;
    }
}