using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Kwetter.Library.Messaging.Datatypes;
using RegisterOrchService.Core.Messaging;
using RegisterOrchService.Core.OrchestrationModels.UserAuth;
using RegisterOrchService.Core.OrchestrationModels.UserProfile;
using RegisterOrchService.Core.Services.Models;
using RegisterOrchService.Core.ViewModel;
using RegisterOrchService.Core.ViewModel.ResponseBody;


namespace RegisterOrchService.Core.Services;

public class RegisterOrchServiceCore(IMapper mapper, IMessageHandler handler) : IRegisterOrchService
{
    private readonly string userAuthURL = "https://localhost:7272/User/create";
    private readonly string userProfileURL = "https://localhost:7106/profiles/Create";

    public async Task<bool> Register(RegisterRequestBody body)
    {
        UserAuth auth = new()
        {
            CorreletionID = Guid.NewGuid(),
            UserID = Guid.NewGuid()
        };

        UserProfile profile = new()
        {
            CorreletionID = auth.CorreletionID,
            UserID = auth.UserID
        };
        if (body.Password != body.ConfirmPassword)
        {
            return false;
        }

        mapper.Map(body, auth);
        mapper.Map(body, profile);
        string userJson = JsonSerializer.Serialize(auth);
        string profileJson = JsonSerializer.Serialize(profile);
        var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
        var profileContent = new StringContent(profileJson, Encoding.UTF8, "application/json");
        
        using HttpClient client = new();
        {
            try
            {
                HttpResponseMessage authTask = await client.PostAsync(userAuthURL, userContent);
                if (authTask.IsSuccessStatusCode)
                {
                    HttpResponseMessage profileTask = await client.PostAsync(userProfileURL, profileContent);
                    if (profileTask.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                handler.SendStatus(new UserMessageBody() {CorreletionID = auth.CorreletionID, Status = Status.Failed, UserID = auth.UserID});
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("error");
                return false;
            }
        }
        return false;
    }
    
}