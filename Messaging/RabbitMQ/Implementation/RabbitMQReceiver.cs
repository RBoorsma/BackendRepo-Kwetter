using System.Text;
using System.Text.Json;
using Kwetter.Library.Messaging.Datatypes;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Kwetter.Library.Messaging.Events;

namespace Messaging.RabbitMQ;

public class RabbitMQReceiver<T>(IMQConnection mqConnection) : IRabbitMQReceiver<T>
{
    public event EventHandler<MessageReceivedEventArgs<T>>? MessageReceived;

    private IModel? channel;

    [Obsolete("This method is not implemented", true)]
    public void StartListeningToMultiple(List<MessageQueue> queues, T requestbody, bool autoAck = true,
        bool seprateConsumer = true)
    {
        throw new NotImplementedException();
        // StartConnection();
        // Console.WriteLine(" [*] Waiting for messages.");
        // if (seprateConsumer)
        // {
        //     foreach (MessageQueue queue in queues)
        //     {
        //         EventingBasicConsumer consumers = SetupListener();
        //         StartListeningTo(queue, requestbody, autoAck);
        //     }
        // }
        // else
        // {
        //     EventingBasicConsumer consumer = SetupListener();
        //     foreach (MessageQueue queue in queues)
        //     {
        //         StartListeningTo(queue, requestbody, autoAck);
        //     }
        // }
    }

    private void StartConnection()
    {
        if (!mqConnection.Connection.IsOpen)
            mqConnection.OpenConnection();
        channel = mqConnection.Connection.CreateModel();
    }

    public void StartListeningToCustom(string queue, bool autoAck = true)
    {
        if (mqConnection.Connection is not { IsOpen: true } || channel == null)
            StartConnection();

        
        EventingBasicConsumer consumer = SetupListener();
        ConsumeChannel(queue, autoAck, consumer);
    }
    public void StartListeningTo(MessageQueue queue, bool autoAck = true)
    {
        if (mqConnection.Connection is not { IsOpen: true } || channel == null)
            StartConnection();
        
        EventingBasicConsumer consumer = SetupListener();
        ConsumeChannel(queue, autoAck, consumer);
    }

    public bool NewConnection()
    {
        try
        {
            IMQConnection connection = new MQConnection(mqConnection.Configuration);
            mqConnection = connection;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    private EventingBasicConsumer SetupListener()
    {
        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            T? data = JsonSerializer.Deserialize<T>(message);
            OnMessageReceived(data, ea.RoutingKey);
        };
        return consumer;
    }

    private void ConsumeChannel(string queue, bool autoAck, EventingBasicConsumer consumer)
    {
        try
        {
            channel.BasicConsume(queue: queue,
                autoAck: autoAck,
                consumer: consumer);
            Console.WriteLine($"Consuming Channel {queue}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Queue {queue.ToString()} doenst exist, please declare your queues first");
          
        }
    }

    
   
    private void ConsumeChannel(MessageQueue queue, bool autoAck, EventingBasicConsumer consumer)
    {
        try
        {
            channel.BasicConsume(queue: queue.ToString(),
                autoAck: autoAck,
                consumer: consumer);
            Console.WriteLine($"Consuming Channel {queue.ToString()}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Queue {queue.ToString()} doenst exist, please declare your queues first");
            
        }

       
    }


    private void OnMessageReceived(T data, string routingkey)
    {
        Console.WriteLine("[x] Received {0}", data);
        MessageReceived?.Invoke(this, new MessageReceivedEventArgs<T>(data, routingkey));
    }

    public void Dispose()
    {
        mqConnection.CloseConnection();
        mqConnection.Connection?.Dispose();
        channel?.Dispose();
    }
}