using Application.EstateApp.EstateDtos;
using Domain.DomainRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateServices.IEstateManagerServices
{
    public interface IEstateManagerService
    {
        Task<RuleResult> ValidateAndCreateEstate(EstateDto estateDto, EstateCreationDto estateCreationDto);
    }
}
