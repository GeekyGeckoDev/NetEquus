using System.Security.Claims;
using System.Text.Json;
using Application.Responses;
using Application.UserApp.IAuthServices;
using Application.UserApp.UserDtos;
using UI.Services.Authentication;

namespace UI.Services.Authentication
{


    public class AuthDataService : IAuthDataService
    {
        private readonly AuthService _authService;

        public AuthDataService(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Calls the API with email/password, updates AuthService state, and stores JWT in localStorage
        /// </summary>
        public async Task<ServiceResponse<ClaimsPrincipal>> LogInAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                return new ServiceResponse<ClaimsPrincipal>("Email is required");

            if (string.IsNullOrEmpty(password))
                return new ServiceResponse<ClaimsPrincipal>("Password is required");

            try
            {
                // Call AuthService API-based login
                var response = await _authService.LogInAsync(email, password);

                if (!response.Success)
                    return response; // return the error

                // At this point AuthService.CurrentUser and CurrentToken are updated
                Console.WriteLine($"[AuthDataService] User logged in: {response.Data?.Identity?.Name ?? "Anonymous"}");

                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ClaimsPrincipal>($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs out the current user
        /// </summary>
        public async Task LogoutAsync()
        {
            await _authService.LogoutAsync();
            Console.WriteLine("[AuthDataService] User logged out");
        }
    }
}