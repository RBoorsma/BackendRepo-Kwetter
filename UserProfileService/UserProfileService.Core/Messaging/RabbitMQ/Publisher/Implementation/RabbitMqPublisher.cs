using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserProfileService.Core.Service;
using UserProfileService.DAL.Repository;


  
namespace UserProfileService.Core.Messaging.RabbitMQ;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    public ConnectionFactory Factory = new ConnectionFactory() {HostName = "localhost", UserName = "user", Password = "password"};
    public  void SendProfileStatus(Guid CorrID, string jsonData)
    {
        
        IConnection connection = Factory.CreateConnection();
        IModel channel = connection.CreateModel();
        channel.QueueDeclare(queue: "Registration", durable: true, exclusive: false, autoDelete: false, arguments: null);
        IBasicProperties properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.CorrelationId = CorrID.ToString();
        string message = $"[x] Send data: {jsonData}, CorreletionId: {CorrID}";
        byte[] body = Encoding.UTF8.GetBytes(jsonData);
        channel.BasicPublish(exchange: string.Empty, routingKey: "Registration", basicProperties:null, body: body);
        Console.WriteLine($"Send {body} to {connection.ToString()}");
    }
    
    
}

