using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserService.Core.Messaging.Models;
using UserService.Core.Services.Events;



namespace UserService.Core.Messaging.RabbitMQ;

public class RabbitMqConsumer : IRabbitMqConsumer
{
    public ConnectionFactory Factory = new ConnectionFactory() {HostName = "localhost", UserName = "user", Password = "password"};
    public event EventHandler<MessageReceivedEventArgs> MessageReceived;
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
            OnMessageReceived(data);
            Console.WriteLine($" [x] Received {data}");
        };
        channel.BasicConsume(queue: "Registration",
            autoAck: true,
            consumer: consumer);
    }

    private void OnMessageReceived(UserRequestBody data)
    {
        MessageReceived?.Invoke(this, new MessageReceivedEventArgs(data));
    }
    
}

