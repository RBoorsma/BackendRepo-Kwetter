using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public interface IMQConnection
{
    void CloseConnection();
    void OpenConnection();
   internal IConnection Connection { get; }
}