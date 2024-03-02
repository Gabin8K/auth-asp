using CustomAuth.DataModels;
using CustomAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route($"[action]")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var result = await _authService.Register(registerModel);
            return Ok(result);
        }

        [HttpPost]
        [Route($"[action]")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _authService.Login(loginModel);
            return Ok(user);
        }
    }
}