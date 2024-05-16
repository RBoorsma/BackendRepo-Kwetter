using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class RegisterController
{
    [ApiController]
    [Route("register")]
    public class UserProfileController : ControllerBase
    {
    
        [HttpPost("/user")]
        public async Task<IActionResult> Register()
        {
            
            return Created();
        }   
   
    
    }
}