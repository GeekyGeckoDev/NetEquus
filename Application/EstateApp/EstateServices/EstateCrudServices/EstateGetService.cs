using Application.EstateApp.IEstateRepos;
using Application.EstateApp.IEstateServices.IEstateCrudServices;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateServices.EstateCrudServices
{
    public class EstateGetService : IEstateGetService
    {
        private readonly IEstateGetRepository _estateGetRepository;

        public EstateGetService(IEstateGetRepository estateGetRepository)
        {
            _estateGetRepository = estateGetRepository;
        }

        public async Task<EquineEstate?> GetEstateByIdAsync(Guid estateId)
        {
            return await _estateGetRepository.GetEstateByIdAsync(estateId);

        }
    }
}
