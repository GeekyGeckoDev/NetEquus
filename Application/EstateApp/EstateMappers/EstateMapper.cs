using Application.EstateApp.EstateDtos;
using Application.UserApp.UserDtos;
using Domain.Entities.Models.EquineEstates;
using Domain.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateMappers
{
    public static class EstateMapper
    {
        public static EstateDto ToDto (EquineEstate equineEstate)
        {
            return new EstateDto
            {
                EstateId = equineEstate.EstateId,
                EstateName = equineEstate.EstateName,
                EstateDesciption = equineEstate.EstateDescription,
                HorseCapacity = equineEstate.HorseCapacity,
                CurrentBalance = equineEstate.CurrentBalance,
                IsSytemEstate = equineEstate.IsSytemEstate
            };
        }

        public static EquineEstate ToEstate (EstateDto estateDto)
        {
            return new EquineEstate
            {
                EstateId = estateDto.EstateId,
                EstateName = estateDto.EstateName,
                EstateDescription = estateDto.EstateDesciption,
                HorseCapacity = estateDto.HorseCapacity,
                CurrentBalance = estateDto.CurrentBalance,
                IsSytemEstate = estateDto.IsSytemEstate
            };
        }

        public static EquineEstate ToCreationEstate (EstateCreationDto estateCreationDto)
        {
            return new EquineEstate
            {
                EstateName = estateCreationDto.EstateName,
                EstateDescription = estateCreationDto.Description,
                HorseCapacity = estateCreationDto.StartHorseCapacity,
                CurrentBalance = estateCreationDto.StartBalance,

            };
        }
    }
}