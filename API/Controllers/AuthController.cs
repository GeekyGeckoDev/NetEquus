using Microsoft.AspNetCore.Mvc;
using Application.UserApp.IUserServices;
using Application.UserApp.UserDtos;
using API.Services;

namespace NetEquus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogInService _loginService;
        private readonly IJwtService _jwt;

        public AuthController(ILogInService loginService, IJwtService jwt)
        {
            _loginService = loginService;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInModel dto)
        {
            var user = await _loginService.ValidateUserAsync(dto.Email, dto.Password);
            if (user == null) return Unauthorized();

            var token = _jwt.GenerateJwt(user);

            return Ok(new
            {
                token = token,
                user = user
            });
        }
    }
}