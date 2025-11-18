using Application.EstateApp.EstateDtos;
using Application.EstateApp.EstateMappers;
using Application.EstateApp.IEstateRepos;
using Application.EstateApp.IEstateServices;
using Application.EstateApp.IEstateServices.IEstateCrudServices;
using Application.EstateApp.IEstateServices.IEstateManagerServices;
using Application.EstateApp.IEstateServices.IEstateOrchestrationServices;
using Application.EstateApp.IEstateServices.IEstateValidationServices;
using Application.SharedApp.IOwnershipServices;
using Application.SharedApp.OwnershipDtos;
using Application.SharedApp.OwnershipMappers;
using Application.UserApp.UserDtos;
using Domain.DomainRules;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateServices.EstateManagerServices
{
    public class EstateOrchestrationService : IEstateOrchestrationService
    {
        private readonly IClientEstateCrudService _clientEstateCrudService;
        private readonly IEstateOwnershipCrudService _ownershipEstateCrudService;
        private readonly IEstateInitilizationService _estateInitilizationService;
        private readonly IEstateOrchestrationValidationService _orchestrationValidationService;
        private readonly IEstateOwnershipInitilizationService _estateOwnershipInitilizationService;
        private readonly IEstateGetService _estateGetService;

        public EstateOrchestrationService(IClientEstateCrudService clientEstateCrudService, IEstateOwnershipCrudService ownershipEstateCrudService, IEstateInitilizationService estateInitilizationService, IEstateOrchestrationValidationService orchestrationValidationService, IEstateOwnershipInitilizationService estateOwnershipInitilizationService, IEstateGetService estateGetService)
        {
            _clientEstateCrudService = clientEstateCrudService;
            _ownershipEstateCrudService = ownershipEstateCrudService;
            _estateInitilizationService = estateInitilizationService;
            _orchestrationValidationService = orchestrationValidationService;
            _estateOwnershipInitilizationService = estateOwnershipInitilizationService;
            _estateGetService = estateGetService;
        }

        public async Task<RuleResult> CreateEstateWithOwnership(
            EstateOwnershipDto estateOwnershipDto,
            EstateCreationDto estateCreationDto,
            UserDto userDto)
        {
            // 1️⃣ Validation
            var validationCheck = await _orchestrationValidationService.FinalValidationAsync(
                estateOwnershipDto, estateCreationDto);

            if (!validationCheck.IsAllowed)
                return validationCheck;

            await _estateInitilizationService.EstateInitializationAsync(estateCreationDto);
            
            var estate = EstateMapper.ToCreationEstate(estateCreationDto);
            await _clientEstateCrudService.CreateEstateAsync(estate);

            // 3️⃣ Assign the saved EstateId to the DTO
            estateOwnershipDto.EquineEstateId = estate.EstateId;

            await _estateOwnershipInitilizationService.LinkUserToEstateAsync(estateOwnershipDto);
            var owner = EstateOwnershipMapper.ToEstateOwner(estateOwnershipDto, userDto);

            Console.WriteLine($"EstateId = {estateOwnershipDto.EquineEstateId}");
            await _ownershipEstateCrudService.CreateEstateOwnershipAsync(owner);

            return RuleResult.Success();
        }

        public async Task<EstateDto> GetConvertEstateAsync (Guid estateId)
        {
            var Getestate = await _estateGetService.GetEstateByIdAsync(estateId);

            var estate = EstateMapper.ToDto(Getestate);

            return estate;
        }

    }
}
