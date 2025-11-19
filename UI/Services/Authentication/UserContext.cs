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
        public EstateDto? EstateId { get; private set; }

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

            if (user.Identity?.IsAuthenticated == true);
    {
                // Use NameIdentifier instead of non-existent "UserGuidId"
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null) return; // or throw
                UserId = Guid.Parse(userIdClaim);

                // Now fetch estate for this user
                EstateId = await _estateOwnershipOrchestrationService.GetMapEstateOwnership(UserId);
            }
        }
    }
}
