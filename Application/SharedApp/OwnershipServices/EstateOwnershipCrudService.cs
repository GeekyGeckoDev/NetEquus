using Application.SharedApp.IOwnershipRepos;
using Application.SharedApp.IOwnershipServices;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.OwnershipServices
{
    public class EstateOwnershipCrudService : IEstateOwnershipCrudService
    {
        private readonly IEstateOwnersipCrudRepository _estateOwnershipRepository;

        public EstateOwnershipCrudService(IEstateOwnersipCrudRepository estateOwnershipRepository)
        {
            _estateOwnershipRepository = estateOwnershipRepository;
        }

        public async Task CreateEstateOwnershipAsync (EstateOwner estateOwner)
        {
            await _estateOwnershipRepository.CreateEstateOwnershipAsync(estateOwner);

        }

        public async Task UpdateEstateOwnershipAsync (EstateOwner estateOwner)
        {
            await _estateOwnershipRepository.UpdateEstateOwnershipAsync(estateOwner);
        }

        public async Task DeleteOwnershipAsync (EstateOwner estateOwner)
        {
            await _estateOwnershipRepository.DeleteEstateOwnershipAsync(estateOwner);
        }
    }
}
