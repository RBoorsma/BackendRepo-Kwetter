using AutoMapper;
using Kwetter.Library.Messaging.Datatypes;
using UserProfileService.Core.Messaging.Handler;
using UserProfileService.Core.Messaging.Models;
using UserProfileService.DAL.Model;
using UserProfileService.DAL.Repository;
using UserProfileService.Core.ViewModel.ResponseBody;

namespace UserProfileService.Core.Service;

public class UserProfileServiceCore(
    IUserProfileRepository userRepository,
    IMapper mapper,
    IMessageHandler messageHandler) : IUserProfileService
{
    public async Task<bool> Create(NewProfileRequestBody body)
    {
        MessagingBody messageData = new();
        try
        {
            if (await userRepository.Create(mapper.Map<UserProfile>(body)))
            {
                messageData.ID = body.UserID;
                messageData.Status = Status.Created.ToString();
                messageHandler.SendStatusCustom(messageData, "profile.created");
                return true;
            }

            
            return false;
        }
        catch(Exception)
        {
            return false;
        }
    }


    public async Task RollbackOrDeleteCreation(Guid UserID, Guid Correletion)
    {
        try
        {
            Task<bool> result = userRepository.RollBackOrDeleteAsync(UserID);
            if (await result)
            {
            }
        }
        catch (Exception)
        {
        }
    }
}