using Kwetter.Library.Messaging.Datatypes;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public interface IPublishData
{
    Exchanges Exchange { get;  }
    string RoutingKey { get; }
    IMessage data { get; }
     QueueOptions? QueueOptions { get;  }
    Guid CorreletionID { get; }
    IBasicProperties? Properties { get;  }

    public T GetData<T>();
}


