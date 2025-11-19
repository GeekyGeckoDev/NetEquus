using Application.Responses;
using Application.UserApp.AuthServices;
using Application.UserApp.IAuthServices;
using Application.UserApp.UserDtos;
using Domain.Entities.Models.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using UI.Services.Authentication;

namespace UI.Components.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IAuthService AuthService { get; set; }
        [Inject] private IAuthDataService AuthDataService { get; set; }

        protected SignInModel loginModel = new();

        protected string DisplayError { get; set; } = "none;";
        protected string errorMessage { get; set; } = "";
        public bool showPassword { get; set; }
        public string? passwordType { get; set; }

        protected InputText? inputTextFocus;
        private string? returnUrl;

        protected override void OnInitialized()
        {
            passwordType = "password";
            showPassword = false;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Extract returnUrl from query string
                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url))
                {
                    returnUrl = url;
                }

                // Attempt restore from token
                if (!AuthService.IsLoggedIn)
                {
                    var restored = await AuthService.RestoreFromLocalStorage();
                    if (restored && !string.IsNullOrEmpty(returnUrl))
                    {
                        NavigationManager.NavigateTo(returnUrl);
                        returnUrl = null;
                    }
                }
            }
        }

        protected void HandlePassword()
        {
            if (showPassword)
            {
                passwordType = "password";
                showPassword = false;
            }
            else
            {
                passwordType = "text";
                showPassword = true;
            }
        }

        protected async Task HandleLogin()
        {
            // 1. Call API through AuthDataService
            var loginResult = await AuthDataService.LogInAsync(loginModel.Email, loginModel.Password);

            if (!loginResult.Success || loginResult.Data == null)
            {
                errorMessage = loginResult.Message ?? "Invalid login";
                DisplayError = "block;";
                return;
            }

            var principal = loginResult.Data;

            // 2. Update Blazor auth state + localStorage token
            var result = await AuthService.LogInAsync(loginModel.Email, loginModel.Password);

            if (!result.Success || result.Data == null)
            {
                errorMessage = result.Message ?? "Login failed";
                DisplayError = "block;";
                return;
            }

            // At this point, AuthService.CurrentUser is already updated
            Console.WriteLine($"Logged in user: {AuthService.CurrentUser.Identity?.Name}");

            // 3. Go to returnUrl if present, otherwise home
            NavigationManager.NavigateTo(returnUrl ?? "/", forceLoad: true);
        }
    }
}