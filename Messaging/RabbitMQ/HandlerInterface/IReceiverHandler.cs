using Kwetter.Library.Messaging.Events;

namespace Messaging.RabbitMQ.HandlerInterface;

public interface IReceiverHandler<T>
{
    void OnMessageReceived(object? sender, MessageReceivedEventArgs<T> eventArgs);

    public void StartListening();
}