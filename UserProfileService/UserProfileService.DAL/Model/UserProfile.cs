using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace UserProfileService.DAL.Model;

public class UserProfile
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [DataMember]
    public Guid ProfileID { get; set; }

    [DataMember][Required] public string? Firstname { get; set; }
    [DataMember][Required] public string? Lastname { get; set; }

    [DataMember][Required] public string? Username { get; set; }
    [DataMember][Required] public Guid UserID { get; set; }
     [DataMember][MaxLength(160)] public string? Bio { get; set; }
}