using Microsoft.AspNetCore.Mvc;
using UserProfileService.Core.Service;
using UserProfileService.Core.ViewModel.ResponseBody;

namespace UserProfileService.Controllers;

[ApiController]
[Route("profiles")]
public class UserProfileController(IUserProfileService userProfileService) : ControllerBase
{
    [HttpPost("Create")]
    public async Task<IActionResult> Create(NewProfileRequestBody body)
    {
        try
        {
            if (await userProfileService.Create(body))
                return Ok();


            return StatusCode(500);
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
    }

    [HttpPost("Serverless")]
    public async Task<IActionResult> Serverless()
    {
        return Ok(await userProfileService.Serverless());
    }
    
    [HttpPost("LoadTest")]
    public async Task<IActionResult> LoadTestDemo(ProfileRequestBody body)
    {
        try
        {
            ProfileResponseBody? profile = await userProfileService.LoadTestDemo(body);
            if (profile != null)
                return Ok(profile);

            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
    }

    [HttpPost("GetProfile")]
    public async Task<IActionResult> GetProfile(ProfileRequestBody body)
    {
        try
        {
            ProfileResponseBody? profile = await userProfileService.GetProfile(body);
            if (profile != null)
                return Ok(profile);

            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
    }
    [HttpPost("GetProfileByUserID")]
    public async Task<IActionResult> GetProfileByUserID(ProfileByUserRequestBody body)
    {
        try
        {
            ProfileResponseBody? profile = await userProfileService.GetProfileByUserID(body);
            if (profile != null)
                return Ok(profile);

            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
    }
}