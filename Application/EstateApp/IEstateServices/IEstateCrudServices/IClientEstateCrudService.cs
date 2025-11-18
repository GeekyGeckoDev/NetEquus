using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateServices.IEstateCrudServices
{
    public interface IClientEstateCrudService
    {
        Task CreateEstateAsync(EquineEstate equineEstate);

        Task UpdateEstateAsync(EquineEstate existingEstate, EquineEstate updatedEstate);


    }
}
