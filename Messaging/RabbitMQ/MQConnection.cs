using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public class MQConnection
{
    private ConnectionFactory Factory;
    internal IConnection _connection { get; private set; }

    public MQConnection()
    {
        Factory = new ConnectionFactory
        {
            HostName = RabbitMQData.hostname,
            UserName = RabbitMQData.user,
            Password = RabbitMQData.password
        };
        _connection = Factory.CreateConnection();
    }

    public void CloseConnection()
    {
        if (_connection is not {IsOpen: false})
            _connection.Close();
    }

    public void OpenConnection()
    {
        if (_connection is not {IsOpen: true})
            _connection = Factory.CreateConnection();
    }
}