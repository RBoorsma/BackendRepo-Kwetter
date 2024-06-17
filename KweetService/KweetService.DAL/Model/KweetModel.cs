using System.ComponentModel.DataAnnotations;

namespace KweetService.DAL.Model;

public class KweetModel
{
    public Guid KweetID { get; set; }
    public Guid ProfileID { get; set; }
    [MaxLength(140)] public string Message { get; set; }
    public DateTime DateCreated { get; set; }
}