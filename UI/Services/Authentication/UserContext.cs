using Application.EstateApp.EstateDtos;
using Application.EstateApp.IEstateAuthService;
using Application.SharedApp.IOwnershipServices;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace UI.Services.Authentication
{
    public class UserContext : IUserContext
    {
        private readonly IEstateOwnershipOrchestrationService _estateOwnershipOrchestrationService;
        private readonly AuthenticationStateProvider _authStateProvider;

        public Guid UserId { get; private set; }
        public EstateDto? Estate { get; private set; }

        public UserContext(
            IEstateOwnershipOrchestrationService orchestrationService,
            AuthenticationStateProvider authStateProvider)
        {
            _estateOwnershipOrchestrationService = orchestrationService;
            _authStateProvider = authStateProvider;
        }

        public async Task InitializeAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            // No semicolon here!
            if (user.Identity?.IsAuthenticated == true)
            {
                // Read the user ID from the JWT
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                    return;

                UserId = Guid.Parse(userIdClaim);

                // Load the user's estate
                Estate = await _estateOwnershipOrchestrationService.GetMapEstateOwnership(UserId);
            }
        }
    }
}