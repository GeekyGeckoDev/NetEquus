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
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using UI.Components;
using UI.Services.Authentication;
using UI.Services.CustomSessionServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Authentication
builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Prevent automatic redirect to /Account/Login
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401; // Let Blazor handle login instead
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // redirect unauthorized users
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

// Add DbContext
builder.Services.AddDbContext<NetEquusDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUserCrudRepository, UserCrudRepository>();
builder.Services.AddScoped<IUserValidationRepository, UserValidationRepository>();
builder.Services.AddScoped<IUserGetRepository, UserGetRepository>();

builder.Services.AddScoped<IEstateCrudRepository, EstateCrudRepository>();
builder.Services.AddScoped<IEstateGetRepository, EstateGetRepository>();
builder.Services.AddScoped<IEstateValidationRepository, EstateValidationRepository>();
builder.Services.AddScoped<IEstateOwnersipCrudRepository, EstateOwnershipCrudRepository>();
builder.Services.AddScoped<IEstateOwnershipGetRepository, EstateOwnershipGetRepository>();
builder.Services.AddScoped<IEstateOwnershipValidationRepository, EstateOwnershipValidationRepository>();

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
builder.Services.AddScoped<IEstateOwnershipOrchestrationService,  EstateOwnershipOrchestrationService>();
builder.Services.AddScoped<IEstateOwnershipInitilizationService, EstateOwnershipInitilizationService>();
builder.Services.AddScoped<IEstateOwnershipValidationService, EstateOwnershipValidationService>();




builder.Services.AddScoped<IAdminUserCrudService, Admin>();
builder.Services.AddScoped<IClientUserCrudService, Client>();
builder.Services.AddScoped<IUserGetService, UserGetService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();
builder.Services.AddScoped<IRegistrationValidationService, RegistrationValidationService>();
builder.Services.AddScoped<IEmailValidationService, EmailValidationService>();
builder.Services.AddScoped<IPasswordValidationService, PasswordValidationService>();
builder.Services.AddScoped<ILogInService, LogInService>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IAuthDataService, AuthDataService>();
builder.Services.AddScoped<IUserContext , UserContext>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ICustomSessionService, CustomSessionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();