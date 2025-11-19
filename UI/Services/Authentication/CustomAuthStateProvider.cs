using Microsoft.AspNetCore.Components.Authorization;
using Application.UserApp.AuthServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.UserApp.IAuthServices;

namespace UI.Services.Authentication
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _authService;

        public CustomAuthStateProvider(IAuthService authService)
        {
            _authService = authService;

            // Subscribe to changes in the user
            _authService.UserChanged += (newUser) =>
            {
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(newUser)));
            };
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Always return the current ClaimsPrincipal from the AuthService
            ClaimsPrincipal user = _authService.CurrentUser ?? new ClaimsPrincipal(new ClaimsIdentity());
            return Task.FromResult(new AuthenticationState(user));
        }


        public async Task RestoreAsync()
        {
            bool restored = await _authService.RestoreFromLocalStorage();
            var user = _authService.CurrentUser;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}