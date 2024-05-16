using RegisterOrchService.Core.ViewModel;
using RegisterOrchService.Core.ViewModel.ResponseBody;


namespace RegisterOrchService.Core.Services;

public interface IRegisterOrchService
{
     Task<bool> Register(RegisterRequestBody body);
}