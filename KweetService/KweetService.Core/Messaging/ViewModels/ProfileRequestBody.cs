using System.ComponentModel.DataAnnotations;

namespace KweetService.Core.Messaging.ViewModels;

public class ProfileRequestBody
{
    [Required] public Guid _id { get; set; }
}