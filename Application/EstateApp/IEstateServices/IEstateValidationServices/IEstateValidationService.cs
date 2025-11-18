using Application.EstateApp.EstateDtos;
using Domain.DomainRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateServices.IEstateValidationServices
{
    public interface IEstateValidationService
    {
        Task<RuleResult> CheckEstateNameAsync(EstateCreationDto estateCreationDto);
    }
}
