using UserService.Core.Messaging.Models;
using UserService.Core.Services.Events;

namespace UserService.Core.Messaging.Handler;

public interface IMessageHandler
{
    void SendStatus(UserRequestBody body);
    void OnMessageReceived(object? sender, MessageReceivedEventArgs e);
    public void StartListening();
}