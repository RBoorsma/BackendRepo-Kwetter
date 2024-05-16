using RabbitMQ.Client;

namespace UserService.Core.Messaging.RabbitMQ;
using RabbitMQ;
public interface IRabbitMqPublisher
{
    void SendProfileStatus(Guid CorrID, string jsonData);
    
}