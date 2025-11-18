using Application.SharedApp.IOwnershipRepos;
using Application.SharedApp.IOwnershipServices;
using Application.SharedApp.OwnershipDtos;
using Application.SharedApp.OwnershipMappers;
using Domain.DomainRules;
using Domain.DomainRules.EstateRules;
using Domain.DomainRules.SharedRules;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.OwnershipServices
{
    public class EstateOwnershipValidationService : IEstateOwnershipValidationService
    {
        private readonly IEstateOwnershipValidationRepository _estateOwnershipValidationRepository;

        public EstateOwnershipValidationService(IEstateOwnershipValidationRepository estateOwnershipValidationReposiotry)
        {
            _estateOwnershipValidationRepository = estateOwnershipValidationReposiotry;
        }

        public async Task<RuleResult> CheckEstateOwnershipAsync(EstateOwnershipDto estateOwnershipDto)
        {
            bool ownsAnyEstate = await _estateOwnershipValidationRepository
                .UserAlreadyOwnsEstateAsync(estateOwnershipDto.UserId, estateOwnershipDto.EquineEstateId);

            // transient EstateOwner just for the domain rule check
            var estateOwner = new EstateOwner
            {
                UserId = estateOwnershipDto.UserId,
                EstateId = estateOwnershipDto.EquineEstateId
            };

            return EstateOwnershipDelegateCheckAll.CheckAll(
                estateOwner,
                EstateOwnershipRules.UserCanOwnOnlyOneEstate(_ => ownsAnyEstate)
            );
        }
    }
}
