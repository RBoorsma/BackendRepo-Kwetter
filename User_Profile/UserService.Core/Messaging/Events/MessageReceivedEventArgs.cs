using UserService.Core.Messaging.Models;

namespace UserService.Core.Services.Events;

public class MessageReceivedEventArgs : EventArgs
{
    public UserRequestBody Data { get; }

    public MessageReceivedEventArgs(UserRequestBody data)
    {
        Data = data;
    }
}