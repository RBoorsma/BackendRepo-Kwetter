using Kwetter.Library.Messaging.Enums;

namespace Messaging;

public class PublishDataBuilder<T>
{
    private T body;
    private RoutingKey key;
    private Guid correlationid = Guid.NewGuid();
    private Exchanges exchange = Exchanges.Empty;
    private QueueOptions QueueOptions = null;

    public PublishDataBuilder<T> setBody(T body)
    {
        this.body = body;
        return this;
    }

    public PublishDataBuilder<T> setRoutingKey(RoutingKey key)
    {
        this.key = key;
        return this;
    }

    public PublishDataBuilder<T> withCorreletionId(Guid id)
    {
        this.correlationid = id;
        return this;
    }

    public PublishDataBuilder<T> setQueueOptions(QueueOptions options)
    {
        this.QueueOptions = options;
        return this;
    }

    

    public IPublishData<T> build()
    {
        if (body == null || key == null)
        {
            throw new InvalidOperationException(
                "Both message body and queue must be set before creating an instance of PublisData");
        }

        IPublishData<T> _publishData =
            new PublishData<T>(body, key, exchange, QueueOptions, correlationid);
        return _publishData;
    }
}