namespace UserService.Core.Messaging.Models;

public class UserMessageBody
{
    public Guid ID { get; set; }
    public Guid CorreletionID { get; set; }
    public string Status { get; set; }
}