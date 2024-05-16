using RabbitMQ.Client;
using UserProfileService.Core.Service;


namespace UserProfileService.Core.Messaging.RabbitMQ;
using RabbitMQ;
public interface IRabbitMqPublisher
{
    void SendProfileStatus(Guid CorrID, string jsonData);
    
}