using Kwetter.Library.Messaging.Enums;

namespace Messaging;

public interface IPublishData<T>
{
    RoutingKey QueueName { get; }
    T data { get; }
    bool Durable { get;  }
    bool Exclusive { get; }
    bool Persistent { get; }
    Guid CorreletionID { get; }
 

}