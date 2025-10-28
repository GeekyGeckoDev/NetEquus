using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Application.UserApp.DTOs;
using Application.UserApp.LoginApp.IAuthenticationServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using Shared.DTOs;

namespace UI.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthService> _logger;

        private const string TokenKey = "authToken";

        public UserDto CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;

        public AuthService(
            HttpClient httpClient,
            IJSRuntime jsRuntime,
            IHttpContextAccessor httpContextAccessor,
            ILogger<AuthService> logger)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        // ✅ LOGIN METHOD
        public async Task<bool> LoginAsync(string email, string password)
        {
            var loginModel = new { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Login failed for {Email}", email);
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponseDto>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (tokenResponse?.Token == null)
                return false;

            // Save JWT in localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, tokenResponse.Token);

            // Parse JWT into claims
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(tokenResponse.Token);
            var identity = new ClaimsIdentity(jwt.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var user = new ClaimsPrincipal(identity);

            // Set cookie (THIS IS WHERE THE ERROR OCCURS)
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                user,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                });

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenResponse.Token);

            // Save user info
            CurrentUser = new UserDto
            {
                UserId = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                Username = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                Email = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                Roles = jwt.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()
            };

            _logger.LogInformation("User {Email} logged in successfully", email);
            return true;
        }

        // ✅ LOGOUT METHOD
        public async Task LogoutAsync()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                CurrentUser = null;
                _logger.LogInformation("User logged out and token cleared");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging out user");
            }
        }

        // ✅ LOAD TOKEN FROM LOCAL STORAGE ON STARTUP
        public async Task GetStateFromTokenAsync()
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
            if (string.IsNullOrWhiteSpace(token))
            {
                _logger.LogInformation("No token found in localStorage");
                return;
            }

            try
            {
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                CurrentUser = new UserDto
                {
                    UserId = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                    Username = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                    Email = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    Roles = jwt.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList()
                };

                _logger.LogInformation("Restored user state from token for {Email}", CurrentUser.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to parse or restore token state");
            }
        }
    }
}
