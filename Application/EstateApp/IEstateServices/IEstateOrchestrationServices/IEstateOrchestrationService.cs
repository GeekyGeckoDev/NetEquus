using Application.EstateApp.EstateDtos;
using Application.SharedApp.OwnershipDtos;
using Application.UserApp.UserDtos;
using Domain.DomainRules;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateServices.IEstateOrchestrationServices
{
    public interface IEstateOrchestrationService
    {
        Task<RuleResult> CreateEstateWithOwnership(EstateOwnershipDto estateOwnershipDto, EstateCreationDto estateCreationDto, UserDto userDto);

        Task<EstateDto> GetConvertEstateAsync(Guid estateId);
    }
}
