using Application.UserApp.AuthServices;
using Application.UserApp.UserDtos;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.HttpContext;

namespace UI.Services.Authentication
{

    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        const string AuthTokenName = "auth_token";
        public event Action<ClaimsPrincipal>? UserChanged;
        private ClaimsPrincipal? currentUser;


        private readonly IHttpContextAccessor _httpContextAccessor;
        public string? CurrentToken { get; private set; }

        public UserDto? CurrentUserDto { get; private set; }

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
        public async Task<UserDto?> GetCurrentUserDtoAsync()
        {
            // Make sure the JWT is loaded and validated
            await GetStateFromTokenAsync(); // updates CurrentUser internally

            if (CurrentUser.Identity?.IsAuthenticated != true)
                return null;

            // Map ClaimsPrincipal to UserDto
            var idClaim = CurrentUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var nameClaim = CurrentUser.FindFirst(ClaimTypes.Name)?.Value;
            var emailClaim = CurrentUser.FindFirst(ClaimTypes.Email)?.Value;

            if (!Guid.TryParse(idClaim, out var userId))
                return null;

            CurrentUserDto = new UserDto
            {
                UserId = userId,
                Username = nameClaim ?? "",
                Email = emailClaim ?? ""
                // add other properties if needed
            };

            return CurrentUserDto;
        }

        private readonly ICustomSessionService _sessionService;
        private readonly IConfiguration _configuration;
        private readonly IJSRuntime _jsRuntime;

        public AuthService(ICustomSessionService sessionService, IConfiguration configuration, ILocalStorageService localStorageService, IHttpContextAccessor httpContextAccessor, IJSRuntime jSRuntime)
        {
            _sessionService = sessionService;
            _configuration = configuration;
            _localStorage = localStorageService;
            _httpContextAccessor = httpContextAccessor;
            _jsRuntime = jSRuntime;
        }

        public ClaimsPrincipal CurrentUser
        {
            get { return currentUser ?? new(); }
            set
            {
                currentUser = value;
                UserChanged?.Invoke(currentUser);

                if (UserChanged is not null)
                {
                    UserChanged(currentUser);
                }
            }
        }

        public bool IsLoggedIn => CurrentUser.Identity?.IsAuthenticated ?? false;

        public async Task ClearLocalStorageAsync()
        {
            if (_jsRuntime != null)
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AuthTokenName);
            }
        }

        public async Task SignOutCookieAsync()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }

        public async Task LogoutAsync()
        {
            CurrentUser = new ClaimsPrincipal();
            UserChanged?.Invoke(CurrentUser);
            await ClearLocalStorageAsync();
            await SignOutCookieAsync();
        }



        public async Task<bool> GetStateFromTokenAsync()
        {
            bool result = false;

            string authToken = await _localStorage.GetItemAsStringAsync(AuthTokenName);

            if (string.IsNullOrEmpty(authToken))
            {
                // fallback to session storage if needed
                authToken = await _sessionService.GetItemAsStringAsync(AuthTokenName);
            }

            var identity = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    //Ensure the JWT is valid
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

                    tokenHandler.ValidateToken(authToken, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                    result = true;
                }
                catch
                {
                    //If the JWT is invalid, remove it from the session
                    await _sessionService.RemoveItemAsync(AuthTokenName);

                    //This is an anonymous user
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);

            //Update the Blazor Server State for the user
            CurrentUser = user;
            return result;
        }


        public async Task Login(ClaimsPrincipal user)
        {
            //Update the Blazor Server State for the user
            CurrentUser = user;

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // persist across sessions
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
            };

            // Sign in using cookie authentication
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                user,
                authProperties
            );

            //Build a JWT for the user
            var tokenEncryptionKey = _configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(tokenEncryptionKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenHoursString = _configuration.GetSection("AppSettings:TokenHours").Value;
            int.TryParse(_configuration.GetSection("AppSettings:TokenHours").Value, out int tokenHours);
            var token = new JwtSecurityToken(
                claims: user.Claims,
                expires: DateTime.Now.AddHours(tokenHours),
                signingCredentials: creds);

            var handler = new JwtSecurityTokenHandler();
            CurrentToken = handler.WriteToken(token);


            await _localStorage.SetItemAsStringAsync(AuthTokenName, CurrentToken);
        }

        public async Task<bool> RestoreFromLocalStorage()
        {
            try
            {
                var jwt = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", AuthTokenName);
                if (string.IsNullOrEmpty(jwt)) return false;

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                var identity = new ClaimsIdentity(token.Claims, "jwt");
                CurrentUser = new ClaimsPrincipal(identity);

                Console.WriteLine($"[AuthService] Restored user: {CurrentUser.Identity?.Name}");
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}