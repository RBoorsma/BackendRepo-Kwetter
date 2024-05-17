using Kwetter.Library.Messaging.Enums;

namespace Messaging;

public class QueueOptions
{
    public bool Durable { get; set; }
    public bool Exclusive { get; set; }
    public bool Persistent { get; set; }
    public MessageQueue MessageQueue { get; set;  }
}