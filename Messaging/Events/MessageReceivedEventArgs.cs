


namespace Kwetter.Library.Messaging.Events;

public class MessageReceivedEventArgs<T> : EventArgs
{
    public T? Data { get;  }
    public string Routingkey { get;  }

    public MessageReceivedEventArgs(T data, string routingkey)
    {
        Data = data;
        this.Routingkey = routingkey;
    }
}