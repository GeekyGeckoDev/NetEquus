using Application.Responses;
using Application.UserApp.AuthServices;
using Application.UserApp.IAuthServices;
using Application.UserApp.UserDtos;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace UI.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;
        const string AuthTokenName = "auth_token";

        public event Action<ClaimsPrincipal>? UserChanged;
        private ClaimsPrincipal? currentUser;
        public ClaimsPrincipal CurrentUser
        {
            get => currentUser ?? new ClaimsPrincipal();
            set
            {
                currentUser = value;
                UserChanged?.Invoke(currentUser);
            }
        }

        public string? CurrentToken { get; private set; }
        public UserDto? CurrentUserDto { get; private set; }

        public bool IsLoggedIn => CurrentUser.Identity?.IsAuthenticated ?? false;

        public Guid CurrentUserGuid
        {
            get
            {
                var idClaim = CurrentUser?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(idClaim, out var guid))
                    return guid;

                throw new InvalidOperationException("Current user does not have a valid ID.");
            }
        }

        public AuthService(HttpClient http, ILocalStorageService localStorage, IJSRuntime jsRuntime)
        {
            _http = http;
            _localStorage = localStorage;
            _jsRuntime = jsRuntime;
        }

        //Task<ServiceResponse<ClaimsPrincipal>> LogInAsync(string email, string password)

        /// Call your API to login, store JWT, and update ClaimsPrincipal
        public async Task<ServiceResponse<ClaimsPrincipal>> LogInAsync(string email, string password)
        {
            try
            {
                var loginModel = new SignInModel { Email = email, Password = password };
                var response = await _http.PostAsJsonAsync("/api/auth/login", loginModel);

                if (!response.IsSuccessStatusCode)
                    return new ServiceResponse<ClaimsPrincipal>("Invalid login");

                var content = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(content);
                var token = jsonDoc.RootElement.GetProperty("token").GetString()!;
                var userJson = jsonDoc.RootElement.GetProperty("user").GetRawText();

                // Store JWT
                CurrentToken = token;
                await _localStorage.SetItemAsStringAsync(AuthTokenName, CurrentToken);

                // Build ClaimsPrincipal
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                CurrentUser = new ClaimsPrincipal(identity);

                // Map user info
                CurrentUserDto = JsonSerializer.Deserialize<UserDto>(userJson);

                return new ServiceResponse<ClaimsPrincipal>("Login successful", true, CurrentUser);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ClaimsPrincipal>(ex.Message);
            }
        }

        public async Task LogoutAsync()
        {
            CurrentUser = new ClaimsPrincipal();
            CurrentUserDto = null;
            CurrentToken = null;
            UserChanged?.Invoke(CurrentUser);
            await _localStorage.RemoveItemAsync(AuthTokenName);
        }

        /// Restore user from localStorage JWT if present
        public async Task<bool> RestoreFromLocalStorage()
        {
            try
            {
                var token = await _localStorage.GetItemAsStringAsync(AuthTokenName);
                if (string.IsNullOrEmpty(token)) return false;

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                CurrentUser = new ClaimsPrincipal(identity);
                CurrentToken = token;

                Console.WriteLine($"[AuthService] Restored user: {CurrentUser.Identity?.Name}");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserDto?> GetCurrentUserDtoAsync()
        {
            if (CurrentUserDto != null) return CurrentUserDto;
            if (!IsLoggedIn) return null;

            var idClaim = CurrentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var nameClaim = CurrentUser.FindFirst(ClaimTypes.Name)?.Value;
            var emailClaim = CurrentUser.FindFirst(ClaimTypes.Email)?.Value;

            if (!Guid.TryParse(idClaim, out var userId)) return null;

            CurrentUserDto = new UserDto
            {
                UserId = userId,
                Username = nameClaim ?? "",
                Email = emailClaim ?? ""
            };

            return CurrentUserDto;
        }
    }

 
}