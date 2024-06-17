using Kwetter.Library.Messaging.Events;
using UserService.Core.Messaging.Models;

namespace UserService.Core.Messaging.Handler;

public interface IUserMessageHandler
{
    void SendStatus(UserMessageBody body);
    void OnMessageReceived(object? sender, MessageReceivedEventArgs<UserMessageBody> eventArgs);
   void StartListening();
}