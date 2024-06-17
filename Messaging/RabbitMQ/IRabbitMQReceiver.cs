using Kwetter.Library.Messaging.Datatypes;
using Kwetter.Library.Messaging.Events;

namespace Messaging.RabbitMQ;

public interface IRabbitMQReceiver<T>
{
    void StartListeningToMultiple(List<MessageQueue> queues, T requestbody, bool autoAck = true,
        bool seprateConsumer = true);

    void StartListeningToCustom(string queue, bool autoAck = true);
    event EventHandler<MessageReceivedEventArgs<T>>? MessageReceived;
    void Dispose();
    void StartListeningTo(MessageQueue queue, bool autoAck = true);
    bool NewConnection();
}



