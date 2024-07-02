using Kwetter.Library.Messaging.Datatypes;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public class MQConnection : IMQConnection
{
    private ConnectionFactory Factory;
    public IConnection Connection { get; private set; }
    public IConfiguration Configuration { get;  }

    public MQConnection(IConfiguration configuration)
    {
        this.Configuration = configuration;
        Factory = new ConnectionFactory
        {
            // HostName = RabbitMQData.hostname,
            // UserName = RabbitMQData.user,
            // Password = RabbitMQData.password
            //Uri = RabbitMQData.Uri
            Uri = new Uri(Environment.GetEnvironmentVariable("rabbitmq"))
        };
        
        Connection = Factory.CreateConnection();
    }

    public void CloseConnection()
    {
        Console.WriteLine("Closed connection with rabbitmq");
        if (Connection is not {IsOpen: false})
            Connection.Close();
    }

    public void OpenConnection()
    {
        Console.WriteLine("Opened connection with rabbitmq");
        if (Connection is not {IsOpen: true})
            Connection = Factory.CreateConnection();
    }
}