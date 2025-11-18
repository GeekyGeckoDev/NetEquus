using Application.SharedApp.IOwnershipRepos;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.IOwnershipServices
{
    public interface IEstateOwnershipCrudService
    {
        Task CreateEstateOwnershipAsync(EstateOwner estateOwner);

        Task UpdateEstateOwnershipAsync(EstateOwner estateOwner);

        Task DeleteOwnershipAsync(EstateOwner estateOwner);

    }
}
