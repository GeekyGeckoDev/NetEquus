using Application.EstateApp.EstateDtos;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateServices
{
    public interface IEstateInitilizationService
    {
        Task EstateInitializationAsync(EstateCreationDto estateCreationDto);
    }
}
