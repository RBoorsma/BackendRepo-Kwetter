


namespace Kwetter.Library.Messaging.Events;

public class MessageReceivedEventArgs<T> : EventArgs
{
    public T? Data { get;  }

    public MessageReceivedEventArgs(T data)
    {
        Data = data;
    }
}