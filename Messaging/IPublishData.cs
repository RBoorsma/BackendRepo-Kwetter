using Kwetter.Library.Messaging.Enums;

namespace Messaging;

public interface IPublishData<T>
{
    Exchanges Exchange { get;  }
    RoutingKey RoutingKey { get; }
    T data { get; }
     QueueOptions QueueOptions { get;  }
    Guid CorreletionID { get; }
 

}