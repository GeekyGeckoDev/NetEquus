using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.IOwnershipRepos
{
    public interface IEstateOwnersipCrudRepository
    {
        Task CreateEstateOwnershipAsync(EstateOwner estateOwnership);

        Task UpdateEstateOwnershipAsync(EstateOwner estateOwnership);

        Task DeleteEstateOwnershipAsync(EstateOwner estateOwnership);

    }
}
