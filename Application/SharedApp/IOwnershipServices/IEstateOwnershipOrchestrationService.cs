using Application.EstateApp.EstateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.IOwnershipServices
{
    public interface IEstateOwnershipOrchestrationService
    {
        Task<EstateDto> GetMapEstateOwnership(Guid userId);
    }
}
