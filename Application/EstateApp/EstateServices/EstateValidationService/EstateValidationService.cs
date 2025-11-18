using Application.EstateApp.EstateDtos;
using Application.EstateApp.IEstateRepos;
using Application.EstateApp.IEstateServices.IEstateValidationServices;
using Application.UserApp.IUserRepos;
using Application.UserApp.UserDtos;
using Domain.DomainRules;
using Domain.DomainRules.EstateRules;
using Domain.DomainRules.UserRules;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateServices.EstateValidationService
{
    public class EstateValidationService : IEstateValidationService
    {
        private readonly IEstateValidationRepository _estateValidationRepository;

        public EstateValidationService(IEstateValidationRepository estateValidationRepository)
        {
            _estateValidationRepository = estateValidationRepository;
        }

        public async Task<RuleResult> CheckEstateNameAsync (EstateCreationDto estateCreationDto)
        {
            bool exists = await _estateValidationRepository.EstateNameExistsAsync(estateCreationDto.EstateName);

            var estate = new EquineEstate
            {
                EstateName = estateCreationDto.EstateName
            };

            return EstateDelegateCheckAll.CheckAll(estate,
                EstateRules.EstateNameCannotBeEmpty,
                EstateRules.EstateNameAlreadyExists(_ => exists));
        }

    }
}