using Kwetter.Library.Messaging.Datatypes;
using RegisterOrchService.Core.Messaging;

namespace RegisterOrchService.Core.Services.Models;

public class UserMessageBody
{
    public Guid UserID { get; set; }
    public Guid CorreletionID { get; set; }
    public Status Status { get; set; }
}