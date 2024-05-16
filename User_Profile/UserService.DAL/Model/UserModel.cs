using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace UserService.DAL.Model;

public class UserModel
{
    [Key]
    [DataMember]
    public Guid UserID { get; set; }

    [Required] [DataMember] public string Username { get; set; }
    [Required] [DataMember] public string Password { get; set; }
    [Required] [DataMember] public string Email { get; set; }
}