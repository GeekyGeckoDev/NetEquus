using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SharedRepos
{
    public interface IEstateOwnershipGetRepository
    {
        Task<EquineEstate?> GetEstateOwnershipByUserId(Guid userId);
    }
}
