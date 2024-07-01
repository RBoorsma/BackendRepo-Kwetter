using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Kwetter.Library.Messaging.Datatypes;
using Microsoft.Extensions.Configuration;
using RegisterOrchService.Core.Messaging;
using RegisterOrchService.Core.OrchestrationModels.UserAuth;
using RegisterOrchService.Core.OrchestrationModels.UserProfile;
using RegisterOrchService.Core.Services.Models;
using RegisterOrchService.Core.ViewModel;
using RegisterOrchService.Core.ViewModel.ResponseBody;

namespace RegisterOrchService.Core.Services;

public class RegisterOrchServiceCore(IMapper mapper, IMessageHandler handler, IConfiguration configuration) : IRegisterOrchService
{
    private readonly string userAuthURL = configuration["UserURL"];
    private readonly string userProfileURL = configuration["UserProfileURL"];

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
        UserMessageBody messageBody;
        using HttpClient client = new();
        {
            try
            {
                HttpResponseMessage authTask = await client.PostAsync(userAuthURL, userContent);
                if (authTask.IsSuccessStatusCode)
                {
                    try
                    {
                        HttpResponseMessage profileTask = await client.PostAsync(userProfileURL, profileContent);
                        if (profileTask.IsSuccessStatusCode)
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                         messageBody = new()
                        {
                            ID = auth.UserID,
                            CorreletionID = auth.CorreletionID,
                            Status = Status.Failed.ToString()
                        };
                        handler.SendStatus(messageBody);
                    }
                }

                messageBody = new()
                {
                    ID = auth.UserID,
                    CorreletionID = auth.CorreletionID,
                    Status = Status.Failed.ToString()
                };
                handler.SendStatus(messageBody);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        return false;
    }
}