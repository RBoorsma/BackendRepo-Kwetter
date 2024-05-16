using AutoMapper;
using Kwetter.Library.Messaging.Enums;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        await userRepository.Create(mapper.Map<UserProfile>(body));
        UserRequestBody messageData = mapper.Map<UserRequestBody>(body);
        messageData.Status = Status.Created.ToString();
        messageHandler.SendStatus(messageData);
    }

    public async Task RollbackOrDeleteCreation(Guid UserID)
    {
        await RollbackOrDeleteCreation(UserID, Guid.NewGuid());
    }

    public async Task RollbackOrDeleteCreation(Guid UserID, Guid Correletion)
    {
        Task<bool> result = userRepository.RollBackOrDeleteAsync(UserID);

        UserRequestBody messageData = new()
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