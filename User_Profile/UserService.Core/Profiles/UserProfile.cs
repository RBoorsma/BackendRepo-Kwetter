using UserService.Core.Messaging.Models;
using UserService.Core.ViewModel;
using UserService.Core.ViewModel.RequestBody;
using UserService.Core.ViewModel.ResponseBody;
using UserService.DAL.Model;

namespace UserService.Core.Profiles;
using AutoMapper;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequestBody, UserModel>().ReverseMap();
        CreateMap<LoginRequestBody, UserModel>().ReverseMap();
        CreateMap<LoginResponseBody, UserModel>().ReverseMap();
        CreateMap<UserModel, UserMessageBody>();
    }
}