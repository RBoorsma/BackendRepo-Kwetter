using System.ComponentModel.DataAnnotations;

namespace RegisterOrchService.Core.OrchestrationModels.UserProfile;

public class UserProfile
{
    [Required] public Guid CorreletionID { get; set; }
    [Required] public string Firstname { get; set; }
    [Required] public string Lastname { get; set; }
    [Required] public string Username { get; set;  }
    [Required] public Guid UserID { get; set; }
}