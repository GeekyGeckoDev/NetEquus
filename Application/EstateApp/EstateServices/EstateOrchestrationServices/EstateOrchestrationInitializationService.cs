using Application.EstateApp.EstateDtos;
using Application.EstateApp.IEstateServices;
using Application.EstateApp.IEstateServices.IEstateOrchestrationServices;
using Application.SharedApp.IOwnershipServices;
using Application.SharedApp.OwnershipDtos;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateServices.EstateOrchestrationServices
{
    public class EstateOrchestrationInitializationService : IEstateOrchestrationInitializationService
    {
        private readonly IEstateInitilizationService _estateInitilizationService;
        private readonly IEstateOwnershipInitilizationService _estateOwnershipInitilizationService;

        public EstateOrchestrationInitializationService(IEstateInitilizationService estateInitilizationService, IEstateOwnershipInitilizationService ownershipInitilizationService)
        {
            _estateInitilizationService = estateInitilizationService;
            _estateOwnershipInitilizationService= ownershipInitilizationService;
        }
        public async Task InitializationOfEstateOwnership (EstateCreationDto estateCreationDto, EstateOwnershipDto estateOwnershipDto)
        {

            await _estateOwnershipInitilizationService.LinkUserToEstateAsync(estateOwnershipDto);
        }
    }
}
