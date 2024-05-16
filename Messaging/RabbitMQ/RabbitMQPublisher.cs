using System.Text;
using Kwetter.Library.Messaging.Enums;
using RabbitMQ.Client;
using UserProfileService.Core.Messaging.Events;

namespace Messaging.RabbitMQ;

public class RabbitMQPublisher<T>(MQConnection connection)
{
    

    //public event EventHandler<MessageReceivedEventArgs<T>>? MessageReceived;
    private IConnection? _connection;
    private IModel? _channel;
    
    
    private void StartConnection()
    {
       
        _connection = connection._connection;
        _channel = _connection.CreateModel();
    }
    public  void SendProfileStatus(Guid CorrID, string jsonData, RoutingKey queueName)
    {
        
        _channel.QueueDeclare(queue: queueName.ToString(), durable: true, exclusive: false, autoDelete: false, arguments: null);
        IBasicProperties properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.CorrelationId = CorrID.ToString();
        string message = $"[x] Send data: {jsonData}, CorreletionId: {CorrID}";
        byte[] body = Encoding.UTF8.GetBytes(jsonData);
        _channel.BasicPublish(exchange: string.Empty, routingKey: "Registration", basicProperties:null, body: body);
        Console.WriteLine($"Send {body} to {connection.ToString()}");
    }
}