
using UserProfileService.Core.Messaging.Models;

namespace UserProfileService.Core.Messaging.Events;

public class MessageReceivedEventArgs : EventArgs
{
    public UserRequestBody Data { get;  }

    public MessageReceivedEventArgs(UserRequestBody data)
    {
        Data = data;
    }
}