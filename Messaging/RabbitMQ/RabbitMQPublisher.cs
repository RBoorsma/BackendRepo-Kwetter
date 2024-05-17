using System.Text;
using System.Text.Json;
using Kwetter.Library.Messaging.Enums;
using RabbitMQ.Client;
using UserProfileService.Core.Messaging.Events;

namespace Messaging.RabbitMQ;

public class RabbitMQPublisher(MQConnection mqConnection)
{
    //public event EventHandler<MessageReceivedEventArgs<T>>? MessageReceived;
    private IConnection? _connection;
    private IModel? _channel;


    private void StartConnection()
    {
        if (!mqConnection.Connection.IsOpen)
            mqConnection.OpenConnection();
        _channel = mqConnection.Connection.CreateModel();
    }

    public void SendProfileStatus<T>(IPublishData<T> data)
    {
        if (data.QueueOptions != null)
            _channel.QueueDeclare(queue: data.QueueOptions.MessageQueue.ToString(), durable: data.QueueOptions.Durable,
                exclusive: data.QueueOptions.Exclusive, autoDelete: false,
                arguments: null);
        IBasicProperties properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.CorrelationId = data.CorreletionID.ToString();
        string message = $"[x] Send data: {data.data}, CorreletionId: {data.CorreletionID.ToString()}";
        string jsonData = JsonSerializer.Serialize(data.data);
        byte[] body = Encoding.UTF8.GetBytes(jsonData);
        _channel.BasicPublish(exchange: data.Exchange.ToString(), routingKey: data.RoutingKey.ToString(),
            basicProperties: properties, body: body);
        Console.WriteLine($"Send {body} to {mqConnection.ToString()}");
    }
}