using Application.SharedApp.IOwnershipServices;
using Application.SharedApp.OwnershipDtos;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.OwnershipServices
{
    public class EstateOwnershipInitilizationService : IEstateOwnershipInitilizationService
    {
        public async Task LinkUserToEstateAsync (EstateOwnershipDto estateOwnershipDto)
        {
            var ownership = new EstateOwner
            {
                UserId = estateOwnershipDto.UserId,
                EstateId = estateOwnershipDto.EquineEstateId,
                IsPrimaryOwner = estateOwnershipDto.isPrimaryOwner
            };
        }
    }
}
