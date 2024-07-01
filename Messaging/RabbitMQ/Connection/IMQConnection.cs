using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ;

public interface IMQConnection
{
    public IConfiguration Configuration { get;  }
    void CloseConnection();
    void OpenConnection();
   internal IConnection Connection { get; }
}