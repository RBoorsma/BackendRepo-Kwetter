using Azure;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using UserService.Core.Messaging;
using UserService.Core.Services;
using UserService.Core.ViewModel;
using UserService.Core.ViewModel.RequestBody;
using UserService.Core.ViewModel.ResponseBody;
using UserService.DAL.Exceptions;
using UserService.DAL.Model;

namespace UserService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RegisterRequestBody model)
        {
            try
            {
                await userService.Create(model);
            }
            catch (UserAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Something went wrong");
            }

            return Created();
        }

        [HttpPost("GetByLogin")]
        [ProducesResponseType(200, Type = typeof(LoginResponseBody))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByLogin([FromBody] LoginRequestBody loginModel)
        {
            LoginResponseBody? userModel = await userService.GetByLogin(loginModel);
            if (userModel != null)
            {
                return Ok(userModel);
            }

            return NotFound();
        }
    }
}