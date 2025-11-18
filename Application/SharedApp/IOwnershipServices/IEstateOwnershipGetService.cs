using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.IOwnershipServices
{
    public interface IEstateOwnershipGetService
    {
        Task<EquineEstate?> GetEstateOwnershipByUserIdAsync(Guid userId);
    }
}
