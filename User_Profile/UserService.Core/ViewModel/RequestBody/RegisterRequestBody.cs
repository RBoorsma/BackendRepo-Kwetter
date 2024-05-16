namespace UserService.Core.ViewModel.RequestBody;

public class RegisterRequestBody
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public Guid UserID { get; set; }
    public Guid CorreletionID { get; set; }
}