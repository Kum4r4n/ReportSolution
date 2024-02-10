using Identity.Application.Interfaces.IServices;
using Identity.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterRequestModel registerRequestModel)
        {
            var data = await _userService.RegisterUser(registerRequestModel);
            return Ok(data);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            var data = await _userService.Login(loginRequestModel);
            return Ok(data);
        }
    }
}
