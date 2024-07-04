using System.Runtime;
using System.Text;
using System.Text.Json;
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
    public async Task<string> Serverless()
    {
        using (HttpClient client = new())
        {
            HttpResponseMessage response = await client.GetAsync("https://hello-world.robboorsma.workers.dev/");
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                string myanswer = responseContent + " Serverless Function Succesfull!";
                return myanswer;
            }
        }

        return "failed to reach serverless function";
    }
    public async Task<ProfileResponseBody> LoadTestDemo(ProfileRequestBody body)
    {
        UserProfile user = mapper.Map<UserProfile>(body);
        ProfileResponseBody profile = mapper.Map<ProfileResponseBody>(user);
        for(int i = 0; i < 10000; i++)
        {
            profile.ProfileID = Guid.NewGuid();
            for (int j = 0; j < 100; j++)
            {
                //Some random code to make the function a bit slower
               var tempString = Convert.ToBase64String(Encoding.UTF8.GetBytes(profile.ProfileID.ToString()));
            }
        }
        profile.firstName = "Load";
        profile.lastName = "Test";
        return profile;

    }
    public async Task<ProfileResponseBody?> GetProfile(ProfileRequestBody body)
    {
        try
        {
            
            UserProfile profile = await userRepository.GetProfile(mapper.Map<UserProfile>(body));
            return mapper.Map<ProfileResponseBody>(profile);
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<ProfileResponseBody?> GetProfileByUserID(ProfileByUserRequestBody body)
    {
        try
        {
            
            UserProfile profile = await userRepository.GetProfileByUserID(mapper.Map<UserProfile>(body));
            return mapper.Map<ProfileResponseBody>(profile);
        }
        catch (Exception)
        {
            return null;
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