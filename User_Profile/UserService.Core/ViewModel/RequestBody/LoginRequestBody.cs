namespace UserService.Core.ViewModel.RequestBody;

public class LoginRequestBody
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Guid OrchestrationID { get; set; }
    //public string? Username { get; set; }
}