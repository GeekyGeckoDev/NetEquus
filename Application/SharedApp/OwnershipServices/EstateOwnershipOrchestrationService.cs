using Application.EstateApp.EstateDtos;
using Application.EstateApp.EstateMappers;
using Application.SharedApp.IOwnershipServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.OwnershipServices
{
    public class EstateOwnershipOrchestrationService : IEstateOwnershipOrchestrationService
    {
        private readonly IEstateOwnershipGetService _estateOwnershipGetService;

        public EstateOwnershipOrchestrationService(IEstateOwnershipGetService estateOwnershipGetService)
        {
            _estateOwnershipGetService = estateOwnershipGetService;
        }

        public async Task<EstateDto> GetMapEstateOwnership (Guid userId)
        {
            var estate = await _estateOwnershipGetService.GetEstateOwnershipByUserIdAsync (userId);

            var estateDto = EstateMapper.ToDto(estate);

            return estateDto;
        }
    }

}
