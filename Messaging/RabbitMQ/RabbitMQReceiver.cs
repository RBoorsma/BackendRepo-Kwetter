using System.Text;
using System.Text.Json;
using Kwetter.Library.Messaging.Enums;
using Messaging.Exceptions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserProfileService.Core.Messaging.Events;

namespace Messaging.RabbitMQ;

public class RabbitMQReceiver<T>
{
    private readonly ConnectionFactory Factory = new ConnectionFactory()
        {HostName = RabbitMQData.hostname, UserName = RabbitMQData.user, Password = RabbitMQData.password};

    public event EventHandler<MessageReceivedEventArgs<T>>? MessageReceived;
    private IConnection? connection;
    private IModel? channel;
    

    public void StartListeningToMultiple(List<MessageQueue> queues, T requestbody, bool autoAck = true,
        bool seprateConsumer = true)
    {
        StartConnection();
        Console.WriteLine(" [*] Waiting for messages.");
        if (seprateConsumer)
        {
            foreach (MessageQueue queue in queues)
            {
                EventingBasicConsumer consumers = SetupListener();
                StartListeningTo(queue, requestbody, autoAck);
            }
        }
        else
        {
            EventingBasicConsumer consumer = SetupListener();
            foreach (MessageQueue queue in queues)
            {
                StartListeningTo(queue, requestbody, autoAck);
            }
        }
    }

    private void StartConnection()
    {
        connection = Factory.CreateConnection();
        channel = connection.CreateModel();
        
    }

    public void StartListeningTo(MessageQueue key, T requestBody, bool autoAck)
    {
        if (connection is not {IsOpen: true})
            StartConnection();

        Console.WriteLine(" [*] Waiting for messages.");
        EventingBasicConsumer consumer = SetupListener();
        ConsumeChannel(key, autoAck, consumer);
    }

    private EventingBasicConsumer SetupListener()
    {
        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            T? data = JsonSerializer.Deserialize<T>(message);
            OnMessageReceived(data);
        };
        return consumer;
    }


    private void ConsumeChannel(MessageQueue queue, bool autoAck, EventingBasicConsumer consumer)
    {
        Console.WriteLine($"Consuming Channel {queue.ToString()}");
        channel.BasicConsume(queue: queue.ToString(),
            autoAck: autoAck,
            consumer: consumer);
    }
    

    private void OnMessageReceived(T data)
    {
        MessageReceived?.Invoke(this, new MessageReceivedEventArgs<T>(data));
    }

    public void Dispose()
    {
        connection?.Dispose();
        channel?.Dispose();
    }
}