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
using ActionResult = UserService.Core.Contracts.ActionResult;

namespace UserService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RegisterRequestBody model)
        {
            bool result = await userService.Create(model);
            if (await userService.Create(model))
                return Created();
            return StatusCode(500);
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