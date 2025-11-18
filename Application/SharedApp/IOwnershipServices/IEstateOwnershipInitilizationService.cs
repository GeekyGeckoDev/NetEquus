using Application.SharedApp.OwnershipDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.IOwnershipServices
{
    public interface IEstateOwnershipInitilizationService
    {
        Task LinkUserToEstateAsync(EstateOwnershipDto estateOwnershipDto);
    }
}
