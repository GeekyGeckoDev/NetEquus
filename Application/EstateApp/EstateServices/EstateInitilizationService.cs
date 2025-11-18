using Application.EstateApp.EstateDtos;
using Application.EstateApp.IEstateServices;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateServices
{
    public class EstateInitilizationService : IEstateInitilizationService
    {
        public async Task EstateInitializationAsync(EstateCreationDto estateCreationDto)
        {
            var estate = new EquineEstate
            {
                EstateName = estateCreationDto.EstateName,
                CurrentBalance = estateCreationDto.StartBalance,
                HorseCapacity = estateCreationDto.StartHorseCapacity
            };
        }


    }
}


