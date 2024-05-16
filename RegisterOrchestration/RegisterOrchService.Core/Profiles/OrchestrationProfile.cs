using RegisterOrchService.Core.OrchestrationModels.UserAuth;
using RegisterOrchService.Core.OrchestrationModels.UserProfile;
using RegisterOrchService.Core.ViewModel;

namespace RegisterOrchService.Core.Profiles;
using AutoMapper;
public class OrchestrationProfile : Profile
{
    public OrchestrationProfile()
    {
        CreateMap<RegisterRequestBody, UserAuth>();
        CreateMap<RegisterRequestBody, UserProfile>();
    }
}