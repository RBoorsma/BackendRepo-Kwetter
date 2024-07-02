using Kwetter.Library.Messaging.Datatypes;
using Kwetter.Library.Messaging.Events;
using UserProfileService.Core.Messaging.Models;



namespace UserProfileService.Core.Messaging.Handler;

public interface IMessageHandler
{
    void SendStatusCustom(MessagingBody body, string key);
    void SendStatus(MessagingBody body, RoutingKey routingKey = RoutingKey.Registration);
    public void StartListening();
    void DeclareQueue();
}