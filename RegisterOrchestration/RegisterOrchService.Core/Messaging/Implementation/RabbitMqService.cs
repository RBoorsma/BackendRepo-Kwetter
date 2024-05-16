using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RegisterOrchService.Core.Services;
using RegisterOrchService.Core.Services.Models;


namespace RegisterOrchService.Core.Messaging;

public class RabbitMqService : IRabbitMqService
{
    public ConnectionFactory Factory = new ConnectionFactory() {HostName = "localhost", UserName = "user", Password = "password"};
   
    public void Receive()
    {
        IConnection connection = Factory.CreateConnection();
        IModel channel = connection.CreateModel();
        Console.WriteLine(" [*] Waiting for messages.");
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            UserRequestBody? data = JsonSerializer.Deserialize<UserRequestBody>(message);
            if (data == null) return;
            Console.WriteLine($" [x] Received {data}");
        };
        channel.BasicConsume(queue: "Registration",
            autoAck: true,
            consumer: consumer);
    }
    public void SendStatus(Guid CorrID, Guid UserID, Status status)
    {
      
        IConnection connection = Factory.CreateConnection();
        IModel channel = connection.CreateModel();
        channel.QueueDeclare(queue: "Registration", durable: true, exclusive: false, autoDelete: false, arguments: null);
        IBasicProperties properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.CorrelationId = CorrID.ToString();
        var data = new {CorreletionID = CorrID, UserID = UserID, Status=status.ToString()};
        string jsonData = JsonSerializer.Serialize(data);
        string message = $"[x] Send data: {jsonData}, CorreletionId: {CorrID} To: {connection.ToString()}";
        byte[] body = Encoding.UTF8.GetBytes(jsonData);
        
        channel.BasicPublish(exchange: string.Empty, routingKey: "Registration", basicProperties:null, body: body);
        Console.WriteLine(message);
        
    }
}