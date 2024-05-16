using System.ComponentModel.DataAnnotations;

namespace RegisterOrchService.Core.ViewModel;

public class RegisterRequestBody
{
    [Required] public string? Firstname { get; set; }
    [Required] public string? Lastname { get; set; }
    [Required] public string? Email { get; set; }
   
   [Required] public string? Username { get; set; }
   [Required] public string? Password { get; set; }
   [Required] public string? ConfirmPassword { get; set; }
}