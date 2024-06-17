namespace UserProfileService.Core.ViewModel.ResponseBody;

public class NewProfileRequestBody
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set;  }
    public Guid UserID { get; set; }
}