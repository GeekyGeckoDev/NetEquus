using Application.EstateApp.EstateDtos;
using Application.SharedApp.OwnershipDtos;
using Domain.DomainRules;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateServices.IEstateOrchestrationServices
{
    public interface IEstateOrchestrationValidationService
    {
        Task<RuleResult> FinalValidationAsync(EstateOwnershipDto estateOwnershipDto, EstateCreationDto estateCreationDto);
    }
}
