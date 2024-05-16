using UserProfileService.Core.Messaging.Events;
using UserProfileService.Core.Messaging.Models;



namespace UserProfileService.Core.Messaging.Handler;

public interface IMessageHandler
{
    void SendStatus(UserRequestBody body);
    void OnMessageReceived(object? sender, MessageReceivedEventArgs e);
    public void StartListening();
}