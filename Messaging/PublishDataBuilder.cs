using Kwetter.Library.Messaging.Enums;

namespace Messaging;

public class PublishDataBuilder<T>
{
    private T body;
    private RoutingKey key;
    private Guid correlationid = Guid.NewGuid();
    private bool durable = true;
    private bool exclusive = false;
    private bool persistent = true;

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

    public PublishDataBuilder<T> isDurable(bool durable)
    {
        this.durable = durable;
        return this;
    }

    public PublishDataBuilder<T> isExclusive(bool exclusive)
    {
        this.exclusive = exclusive;
        return this;
    }

    public PublishDataBuilder<T> isPersistent(bool persistent)
    {
        this.persistent = persistent;
        return this;
    }


    public IPublishData<T> build()
    {
        if (body == null || key == null)
        {
            throw new InvalidOperationException(
                "Both message body and queue must be set before creating an instance of PublisData");
        }

        IPublishData<T> _publishData = new PublishData<T>(body, key, durable, exclusive, persistent, correlationid);
        return _publishData;
    }
}