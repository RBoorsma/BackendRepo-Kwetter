namespace RegisterOrchService.Core.ViewModel.ResponseBody;

public class RegisterResponseBody
{
    public Guid ProfileID { get; set; }
    public Guid UserID { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
}