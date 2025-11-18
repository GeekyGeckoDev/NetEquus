using System.Security.Claims;
using Application.UserApp.AuthServices;
using Application.UserApp.Exceptions;
using Application.UserApp.IUserServices;
using Application.Responses;

namespace UI.Services.Authentication
{
    public class AuthDataService : IAuthDataService
    {
        private readonly ILogInService _logInService;

        public AuthDataService(ILogInService logInService)
        {
            _logInService = logInService;
        }

        public async Task<ServiceResponse<ClaimsPrincipal>> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                return new ServiceResponse<ClaimsPrincipal>("Email is required");

            if (string.IsNullOrEmpty(password))
                return new ServiceResponse<ClaimsPrincipal>("Password is required");

            try
            {
                var userDto = await _logInService.LoginAsync(email, password);

                if (userDto == null)
                    return new ServiceResponse<ClaimsPrincipal>("Invalid login");

                // Build claims from your UserDto
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userDto.UserId.ToString()),
                    new Claim(ClaimTypes.Name, userDto.Username),
                    new Claim(ClaimTypes.Email, userDto.Email),
                    new Claim(ClaimTypes.Role, userDto.IsAdmin ? "Admin" : "User")
                };

                var identity = new ClaimsIdentity(claims, "apiauth_type"); // "apiauth_type" or "jwt"
                var principal = new ClaimsPrincipal(identity);

                return new ServiceResponse<ClaimsPrincipal>("Login successful", true, principal);
            }
            catch (LoginException ex)
            {
                return new ServiceResponse<ClaimsPrincipal>(ex.Message);
            }
            catch (Exception ex)
            {
                // Optional: log exception somewhere
                return new ServiceResponse<ClaimsPrincipal>("An unexpected error occurred");

            }
        }
    }
}
