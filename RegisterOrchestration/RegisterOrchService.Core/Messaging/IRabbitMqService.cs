using RabbitMQ.Client;

namespace RegisterOrchService.Core.Messaging;
using RabbitMQ;
public interface IRabbitMqService
{
    void Receive();
    void SendStatus(Guid CorrID, Guid UserID, Status status);

}