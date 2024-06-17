using System.IO.Compression;
using Kwetter.Library.Messaging.Datatypes;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ;


public class PublishDataBuilder
{
    private IMessage body;
   // private T body;
  
    private string key;
    private Guid correlationid = Guid.NewGuid();
    private Exchanges exchange = Exchanges.Default;
    private QueueOptions? QueueOptions = null;
    private IBasicProperties? Properties = null;


 
    public PublishDataBuilder setBody<T>(MessageContainer<T> body)
    {
        this.body = body;
        return this;
    }

    public PublishDataBuilder setRoutingKey(RoutingKey key)
    {
        this.key = key.ToString();
        return this;
    }
    public PublishDataBuilder setCustomRoutingKey(string key)
    {
        this.key = key;
        return this;
    }

    public PublishDataBuilder withCorreletionId(Guid id)
    {
        this.correlationid = id;
        return this;
    }

    public PublishDataBuilder setQueueOptions(QueueOptions options)
    {
        this.QueueOptions = options;
        return this;
    }

    public PublishDataBuilder setProperties(IBasicProperties properties)
    {
        this.Properties = properties;
        return this;
    }
    

    public IPublishData build()
    {
        if (body == null || key == null)
        {
            throw new InvalidOperationException(
                "Both message body and queue must be set before creating an instance of PublisData");
        }

        IPublishData _publishData =
            new PublishData(body, key, exchange, QueueOptions, correlationid, Properties);
        return _publishData;
    }
}