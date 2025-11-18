using Application.EstateApp.EstateDtos;
using Application.EstateApp.IEstateServices.IEstateManagerServices;
using Application.EstateApp.IEstateServices.IEstateOrchestrationServices;
using Application.EstateApp.IEstateServices.IEstateValidationServices;
using Application.SharedApp.IOwnershipServices;
using Application.SharedApp.OwnershipDtos;
using Application.SharedApp.OwnershipServices;
using Domain.DomainRules;
using Domain.Entities.Models.EquineEstates;
using Domain.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateServices.EstateManagerServices
{
    public class EstateOrchestrationValidationService : IEstateOrchestrationValidationService
    {
        private readonly IEstateValidationService _estateValidationService;
        private readonly IEstateOwnershipValidationService _estateOwnershipValidationService;

        public EstateOrchestrationValidationService (IEstateValidationService estateValidationService, IEstateOwnershipValidationService estateOwnershipValidationService)
        {
            _estateValidationService = estateValidationService;
            _estateOwnershipValidationService = estateOwnershipValidationService;
        }

        public async Task<RuleResult> FinalValidationAsync (EstateOwnershipDto estateOwnershipDto, EstateCreationDto estateCreationDto )
        {
            var estateCheck = await _estateOwnershipValidationService.CheckEstateOwnershipAsync(estateOwnershipDto);
            if (estateCheck == null || !estateCheck.IsAllowed)
                return estateCheck;

            var estateNameCheck = await _estateValidationService.CheckEstateNameAsync(estateCreationDto);
            if (estateNameCheck == null || !estateNameCheck.IsAllowed)
                return estateNameCheck;

            return RuleResult.Success();


        }
    }
}
