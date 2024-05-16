using System.ComponentModel.DataAnnotations;

namespace RegisterOrchService.Core.OrchestrationModels.UserAuth;

public class UserAuth
{
    [Required] public string Username { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string ConfirmPassword { get; set; }
    [Required] public Guid CorreletionID { get; set; }
    [Required] public Guid UserID { get; set; }
}