using System.Reflection;
using System.Text;
using System.Text.Json;
using Kwetter.Library.Messaging.Datatypes;
using RabbitMQ.Client;
using Kwetter.Library.Messaging.Events;

namespace Messaging.RabbitMQ;

public class RabbitMQPublisher(IMQConnection mqConnection) : IRabbitMQPublisher
{
    //public event EventHandler<MessageReceivedEventArgs<T>>? MessageReceived;
    private IModel? channel;


    private void StartConnection()
    {
        if (!mqConnection.Connection.IsOpen)
            mqConnection.OpenConnection();
        channel = mqConnection.Connection.CreateModel();
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

    public IBasicProperties GetProperties()
    {
        return channel.CreateBasicProperties();
    }

    public bool DeclareQueues(string queue, bool durable = true, bool exclusive = false, bool autoDelete = false)
    {
        if (mqConnection.Connection is not { IsOpen: true } || channel == null)
            StartConnection();
        channel.QueueDeclare(queue: queue, durable: durable,
            exclusive: exclusive, autoDelete: autoDelete,
            arguments: null);
        
        mqConnection.CloseConnection();

        return true;
    }
    public bool SendMessage(IPublishData publishData)
    {
        if (mqConnection.Connection is not { IsOpen: true } || channel == null)
            StartConnection();
        if (publishData.QueueOptions != null)
            channel.QueueDeclare(queue: publishData.QueueOptions.MessageQueue.ToString(), durable: publishData.QueueOptions.Durable,
                exclusive: publishData.QueueOptions.Exclusive, autoDelete: false,
                arguments: null);
        IBasicProperties properties = publishData.Properties ?? channel.CreateBasicProperties();
        if (publishData.Properties == null)
        {
            properties.Persistent = true;
        }

        properties.CorrelationId = publishData.CorreletionID.ToString();

        try
        {
            // Datavalue Retrieves property: T Data from PublishData by getting the Class Type and then retrieving the value.
            var dataValue = publishData.data.GetType().GetProperty("Data").GetValue(publishData.data);

            string jsonData = JsonSerializer.Serialize(dataValue);
            byte[] body = Encoding.UTF8.GetBytes(jsonData);
            channel.BasicPublish(exchange: string.Empty, routingKey: publishData.RoutingKey.ToString(),
                basicProperties: properties, body: body);
            Console.WriteLine(
                $"Send {publishData.data} to {mqConnection.ToString()} with Correlation ID: {publishData.CorreletionID.ToString()}");
            mqConnection.CloseConnection();
            return true;
        }
        catch (Exception)
        {
            mqConnection.CloseConnection();
            Type test = publishData.data.GetType();
            PropertyInfo[] yes = test.GetProperties();
        }

        return false;
    }
}