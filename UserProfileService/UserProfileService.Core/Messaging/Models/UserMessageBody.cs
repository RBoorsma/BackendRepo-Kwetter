namespace UserProfileService.Core.Messaging.Models;

public class UserMessageBody
{
    public Guid UserID { get; set; }
    public Guid CorreletionID { get; set; }
    public string Status { get; set; }

   
}