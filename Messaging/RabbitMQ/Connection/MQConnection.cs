using Kwetter.Library.Messaging.Datatypes;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public class MQConnection : IMQConnection
{
    private ConnectionFactory Factory;
    public IConnection Connection { get; private set; }

    public MQConnection()
    {
        Factory = new ConnectionFactory
        {
            // HostName = RabbitMQData.hostname,
            // UserName = RabbitMQData.user,
            // Password = RabbitMQData.password
            Uri = RabbitMQData.Uri
        };
        
        Connection = Factory.CreateConnection();
    }

    public void CloseConnection()
    {
        if (Connection is not {IsOpen: false})
            Connection.Close();
    }

    public void OpenConnection()
    {
        if (Connection is not {IsOpen: true})
            Connection = Factory.CreateConnection();
    }
}