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
    public async Task Create(NewProfileRequestBody body)
    {
        UserMessageBody messageData = new();
        try
        {
            if (await userRepository.Create(mapper.Map<UserProfile>(body)))
            {
                messageData = mapper.Map<UserMessageBody>(body);
                messageData.Status = Status.Created.ToString();
            }
            else
            {

                messageData.Status = Status.Failed.ToString();
                
            }

        }
        catch
        {
            messageData.Status = Status.Failed.ToString();
        }
        messageHandler.SendStatus(messageData);

        
        
       
    }

    public async Task RollbackOrDeleteCreation(Guid UserID)
    {
        await RollbackOrDeleteCreation(UserID, Guid.NewGuid());
    }

    public async Task RollbackOrDeleteCreation(Guid UserID, Guid Correletion)
    {
        Task<bool> result = userRepository.RollBackOrDeleteAsync(UserID);

        UserMessageBody messageData = new()
        {
            UserID = UserID,
            CorreletionID = Correletion
        };
        if (await result)
            messageData.Status = Status.Removed.ToString();

        messageData.Status = Status.Failed.ToString();
        messageHandler.SendStatus(messageData);
    }
}