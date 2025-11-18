using Application.UserApp.UserDtos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.AuthServices
{
    public interface IAuthService
    {
        ClaimsPrincipal CurrentUser { get; set; }
        bool IsLoggedIn { get; }
        event Action<ClaimsPrincipal> UserChanged;
        Task<bool> GetStateFromTokenAsync();
        Task LogoutAsync();
        Task Login(ClaimsPrincipal user);

        Task<UserDto?> GetCurrentUserDtoAsync();
        String CurrentToken { get;  }
        Guid CurrentUserGuid { get; }

        Task ClearLocalStorageAsync();

        Task<bool> RestoreFromLocalStorage();
            Task SignOutCookieAsync();
    }
}
