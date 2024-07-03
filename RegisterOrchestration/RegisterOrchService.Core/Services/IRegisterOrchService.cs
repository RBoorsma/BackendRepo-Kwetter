using RegisterOrchService.Core.ViewModel;


namespace RegisterOrchService.Core.Services;

public interface IRegisterOrchService
{
     Task<bool> Register(RegisterRequestBody body);
}