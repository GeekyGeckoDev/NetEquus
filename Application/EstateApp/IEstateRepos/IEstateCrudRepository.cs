using Domain.Entities.Models.EquineEstates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateRepos
{
    public interface IEstateCrudRepository
    {

        Task CreateEstateAsync(EquineEstate newEstate);

        Task UpdateEstateAsync(EquineEstate equineEstate);

        Task DeleteEstateAsync(EquineEstate equineEstate);

    }
}
