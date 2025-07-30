using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service;

namespace GrpcRealTimeAssignment.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var created = await _userService.RegisterAsync(user.Email!, user.PasswordHash!);
            return Ok(created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var found = await _userService.LoginAsync(user.Email!, user.PasswordHash!);
            return found == null ? Unauthorized() : Ok(found);
        }
    }

}
