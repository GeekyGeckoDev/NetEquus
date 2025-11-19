using API.Services;
using Application.EstateApp.EstateServices;
using Application.EstateApp.EstateServices.EstateCrudServices;
using Application.EstateApp.EstateServices.EstateManagerServices;
using Application.EstateApp.EstateServices.EstateOrchestrationServices;
using Application.EstateApp.EstateServices.EstateValidationService;
using Application.EstateApp.IEstateAuthService;
using Application.EstateApp.IEstateRepos;
using Application.EstateApp.IEstateServices;
using Application.EstateApp.IEstateServices.IEstateCrudServices;
using Application.EstateApp.IEstateServices.IEstateOrchestrationServices;
using Application.EstateApp.IEstateServices.IEstateValidationServices;
using Application.SharedApp.IOwnershipRepos;
using Application.SharedApp.IOwnershipServices;
using Application.SharedApp.OwnershipServices;
using Application.UserApp.AuthServices;
using Application.UserApp.IAuthServices;
using Application.UserApp.IUserRepos;
using Application.UserApp.IUserServices;
using Application.UserApp.IUserServices.IUserCrudServices;
using Application.UserApp.IUserServices.IUserValidationServices;
using Application.UserApp.PasswordHasherServices;
using Application.UserApp.UserSevices.UserCrudServices;
using Application.UserApp.UserSevices.UserManagerServices;
using Application.UserApp.UserSevices.UserValidationServices;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Infrastructure;
using Infrastructure.Repositories.EstateRepos;
using Infrastructure.Repositories.SharedRepos;
using Infrastructure.Repositories.UserRepos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UI.Components;
using UI.Services.Authentication;
using UI.Services.CustomSessionServices;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Add Razor / Blazor services
// ---------------------------
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthorization();
builder.Services.AddAuthorizationCore(); // needed for Blazor auth
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();

// ---------------------------
// Authentication & JWT
// ---------------------------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]))
        };
    });

// ---------------------------
// HttpClient for Blazor Server
// ---------------------------
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
});

// ---------------------------
// DB Context
// ---------------------------
builder.Services.AddDbContext<NetEquusDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------------
// Repositories
// ---------------------------
builder.Services.AddScoped<IUserCrudRepository, UserCrudRepository>();
builder.Services.AddScoped<IUserValidationRepository, UserValidationRepository>();
builder.Services.AddScoped<IUserGetRepository, UserGetRepository>();

builder.Services.AddScoped<IEstateCrudRepository, EstateCrudRepository>();
builder.Services.AddScoped<IEstateGetRepository, EstateGetRepository>();
builder.Services.AddScoped<IEstateValidationRepository, EstateValidationRepository>();
builder.Services.AddScoped<IEstateOwnersipCrudRepository, EstateOwnershipCrudRepository>();
builder.Services.AddScoped<IEstateOwnershipGetRepository, EstateOwnershipGetRepository>();
builder.Services.AddScoped<IEstateOwnershipValidationRepository, EstateOwnershipValidationRepository>();

// ---------------------------
// Estate services
// ---------------------------
builder.Services.AddScoped<IAdminEstateCrudService, EstateAdmin>();
builder.Services.AddScoped<IClientEstateCrudService, EstateClient>();
builder.Services.AddScoped<IEstateGetService, EstateGetService>();
builder.Services.AddScoped<IEstateOrchestrationInitializationService, EstateOrchestrationInitializationService>();
builder.Services.AddScoped<IEstateOrchestrationService, EstateOrchestrationService>();
builder.Services.AddScoped<IEstateOrchestrationValidationService, EstateOrchestrationValidationService>();
builder.Services.AddScoped<IEstateValidationService, EstateValidationService>();
builder.Services.AddScoped<IEstateInitilizationService, EstateInitilizationService>();
builder.Services.AddScoped<IEstateOwnershipCrudService, EstateOwnershipCrudService>();
builder.Services.AddScoped<IEstateOwnershipGetService, EstateOwnershipGetService>();
builder.Services.AddScoped<IEstateOwnershipOrchestrationService, EstateOwnershipOrchestrationService>();
builder.Services.AddScoped<IEstateOwnershipInitilizationService, EstateOwnershipInitilizationService>();
builder.Services.AddScoped<IEstateOwnershipValidationService, EstateOwnershipValidationService>();

// ---------------------------
// User services
// ---------------------------
builder.Services.AddScoped<IAdminUserCrudService, Admin>();
builder.Services.AddScoped<IClientUserCrudService, Client>();
builder.Services.AddScoped<IUserGetService, UserGetService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IRegistrationValidationService, RegistrationValidationService>();
builder.Services.AddScoped<IEmailValidationService, EmailValidationService>();
builder.Services.AddScoped<IPasswordValidationService, PasswordValidationService>();
builder.Services.AddScoped<ILogInService, LogInService>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();

// ---------------------------
// Auth services (ORDER MATTERS!)
// ---------------------------

// 1?? AuthService depends on HttpClient, ILocalStorageService, IJSRuntime
builder.Services.AddScoped<IAuthService, AuthService>();

// 2?? Custom Auth State Provider depends on AuthService
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// 3?? AuthDataService depends on AuthService
builder.Services.AddScoped<IAuthDataService, AuthDataService>();

// 4?? UserContext depends on AuthService
builder.Services.AddScoped<IUserContext, UserContext>();

// ---------------------------
// Misc
// ---------------------------
builder.Services.AddScoped<ICustomSessionService, CustomSessionService>();

// ---------------------------
// Build and run
// ---------------------------
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();