using Application.Responses;
using System.Security.Claims;

namespace Application.UserApp.IAuthServices
{
    public interface IAuthDataService
    {
        Task<ServiceResponse<ClaimsPrincipal>> LogInAsync(string email, string password);
        Task LogoutAsync();
    }
}