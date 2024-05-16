namespace UserService.Core.Messaging.Models;

public class UserRequestBody
{
    public Guid UserID { get; set; }
    public Guid CorreletionID { get; set; }
    public string Status { get; set; }
}