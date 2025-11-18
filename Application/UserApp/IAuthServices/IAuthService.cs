using Application.Responses;
using Application.UserApp.UserDtos;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.UserApp.IAuthServices
{
    public interface IAuthService
    {

        ClaimsPrincipal CurrentUser { get; set; }


        bool IsLoggedIn { get; }

     
        event Action<ClaimsPrincipal> UserChanged;

        Task<ServiceResponse<ClaimsPrincipal>> LogInAsync(string email, string password);



        Task LogoutAsync();

        Task<bool> RestoreFromLocalStorage();

        Task<UserDto?> GetCurrentUserDtoAsync();


        string? CurrentToken { get; }


        Guid CurrentUserGuid { get; }
    }
}