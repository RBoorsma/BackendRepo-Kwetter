using System.Text.Json.Serialization;

namespace UserService.Core.ViewModel.ResponseBody;

public class LoginResponseBody
{
    [JsonPropertyName("UserID")]
    public Guid UserID { get; set; }
}