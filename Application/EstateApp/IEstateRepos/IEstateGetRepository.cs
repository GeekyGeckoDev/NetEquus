using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateRepos
{
    public interface IEstateGetRepository
    {
        Task<EquineEstate?> GetEstateByIdAsync(Guid estateId);
    }
}
