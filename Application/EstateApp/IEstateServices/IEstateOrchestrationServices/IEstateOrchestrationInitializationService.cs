using Application.EstateApp.EstateDtos;
using Application.SharedApp.OwnershipDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateServices.IEstateOrchestrationServices
{
    public interface IEstateOrchestrationInitializationService
    {
        Task InitializationOfEstateOwnership(EstateCreationDto estateCreationDto, EstateOwnershipDto estateOwnershipDto);
    }
}
