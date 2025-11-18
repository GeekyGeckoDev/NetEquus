using Application.EstateApp.IEstateRepos;
using Application.EstateApp.IEstateServices.IEstateCrudServices;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateServices
{
    public class EstateCrudService
    {
        protected readonly IEstateCrudRepository _estateCrudRepository;

        public EstateCrudService (IEstateCrudRepository estateCrudRepository)
        {
            _estateCrudRepository = estateCrudRepository;
        }

        public virtual async Task UpdateEstateAsync(EquineEstate existingEstate, EquineEstate updatedEstate)
        {
            existingEstate.EstateName = updatedEstate.EstateName;

            await _estateCrudRepository.UpdateEstateAsync(existingEstate);
        }

    }

    public class EstateAdmin : EstateCrudService, IAdminEstateCrudService
    {
        public EstateAdmin(IEstateCrudRepository estateCrudRepository) : base(estateCrudRepository)
        {
        }

        public async Task DeleteEstateAsync(EquineEstate equineEstate)
        {
            await _estateCrudRepository.DeleteEstateAsync(equineEstate);
        }

        public override async Task UpdateEstateAsync(EquineEstate existingEstate, EquineEstate updatedEstate)
        {
            existingEstate.EstateName = updatedEstate.EstateName;
            existingEstate.EstateOwners = updatedEstate.EstateOwners;
            existingEstate.CurrentBalance = updatedEstate.CurrentBalance;
            existingEstate.HorseCapacity = updatedEstate.HorseCapacity;
            existingEstate.EstateDescription = updatedEstate.EstateDescription;

            await _estateCrudRepository.UpdateEstateAsync(existingEstate);
        }

    }

    public class EstateClient : EstateCrudService, IClientEstateCrudService
    {
        public EstateClient(IEstateCrudRepository estateCrudRepository) : base(estateCrudRepository)
        {
        }

        public async Task CreateEstateAsync(EquineEstate equineEstate)
        {
            await _estateCrudRepository.CreateEstateAsync(equineEstate);
        }

        public virtual async Task UpdateEstateAsync(EquineEstate existingEstate, EquineEstate updatedEstate)
        {
            existingEstate.EstateName = updatedEstate.EstateName;
            existingEstate.EstateDescription = updatedEstate.EstateDescription;
        }
    }
}
