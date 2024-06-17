using Kwetter.Library.Messaging.Events;
using UserProfileService.Core.Messaging.Models;



namespace UserProfileService.Core.Messaging.Handler;

public interface IMessageHandler
{
    void SendStatus(UserMessageBody body);
    public void StartListening();
}