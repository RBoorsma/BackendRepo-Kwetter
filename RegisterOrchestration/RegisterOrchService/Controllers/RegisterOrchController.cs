using Microsoft.AspNetCore.Mvc;
using RegisterOrchService.Core.Services;
using RegisterOrchService.Core.ViewModel;


namespace RegisterOrchService.Controllers;

[ApiController]
[Route("Register")]
public class RegisterOrchController(IRegisterOrchService registerService) : ControllerBase
{
    [HttpPost("NewUser")]
    public async Task<IActionResult> NewUser(RegisterRequestBody body)
    {
        if (await registerService.Register(body))
            return Ok();

        return StatusCode(500);
    }
}