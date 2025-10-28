using Application.UserApp.LoginApp.Entities;
using Application.UserApp.LoginApp.IAuthenticationServices;
using System.Security.Claims;

namespace UI.Components.AuthServices
{

    public class AuthDataService : IAuthDataService
    {
        public ServiceResponse<ClaimsPrincipal> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new ServiceResponse<ClaimsPrincipal>("Email is required");
            }

            if (string.IsNullOrEmpty(password))
            {
                return new ServiceResponse<ClaimsPrincipal>("Password is required");
            }

            if (email != "super@acme.com" && email != "admin@acme.com")
            {
                return new ServiceResponse<ClaimsPrincipal>(
                    "Unknown user, please enter super@acme.com or admin@acme.com");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, email.StartsWith("admin") ? "Admin" : "Super")
            };

            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);

            return new ServiceResponse<ClaimsPrincipal>("Login successful", true, principal);
        }
    }
}
