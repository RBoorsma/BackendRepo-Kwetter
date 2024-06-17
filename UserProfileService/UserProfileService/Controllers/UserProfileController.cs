using Microsoft.AspNetCore.Mvc;
using UserProfileService.Core.Service;
using UserProfileService.Core.ViewModel.ResponseBody;

namespace UserProfileService.Controllers;
[ApiController]
[Route("profiles")]
public class UserProfileController(IUserProfileService userProfileService) : ControllerBase
{
    
    [HttpPost("test")]
    public async Task<IActionResult> RabbitMQ()
    {
       Guid guid = Guid.Parse("6a3a1e2f-7fc8-47db-885b-250030d7a6dd");
        await userProfileService.RollbackOrDeleteCreation(guid);

        return Ok();
    }   
    [HttpPost("Create")]
    public async Task<IActionResult> Create(NewProfileRequestBody body)
    {
        try
        {
            await userProfileService.Create(body);
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }

        return Ok();
    }   
    
}