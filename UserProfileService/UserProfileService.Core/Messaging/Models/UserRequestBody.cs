using Kwetter.Library.Messaging.Enums;
using Messaging;
using Messaging.RabbitMQ;

namespace UserProfileService.Core.Messaging.Models;

public class UserRequestBody
{
    public Guid UserID { get; set; }
    public Guid CorreletionID { get; set; }
    public string Status { get; set; }

   
}