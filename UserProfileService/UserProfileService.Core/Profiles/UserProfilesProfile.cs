using AutoMapper;
using UserProfileService.Core.Messaging.Models;
using UserProfileService.Core.ViewModel.ResponseBody;
using UserProfileService.DAL.Model;


namespace UserProfileService.Core.Profiles;

public class UserProfilesProfile : Profile
{
    public UserProfilesProfile()
    {
        CreateMap<NewProfileRequestBody, UserProfile>();
        CreateMap<NewProfileRequestBody, MessagingBody>();
        CreateMap<UserProfile, ProfileResponseBody>();
    }
}