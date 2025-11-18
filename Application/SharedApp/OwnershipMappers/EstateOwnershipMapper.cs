using Application.SharedApp.OwnershipDtos;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UserApp.UserDtos;

namespace Application.SharedApp.OwnershipMappers
{
    public static class EstateOwnershipMapper
    {
        public static EstateOwnershipDto ToDto(EstateOwner estateOwner)
        {
            return new EstateOwnershipDto
            {
                EquineEstateId = estateOwner.EstateId,
                UserId = estateOwner.UserId,
                isPrimaryOwner = estateOwner.IsPrimaryOwner


            };
        }

        public static EstateOwner ToEstateOwner (EstateOwnershipDto estateOwnershipDto, UserDto userDto)
        {
            return new EstateOwner
            {
                EstateId = estateOwnershipDto.EquineEstateId,
                UserId = userDto.UserId,
                IsPrimaryOwner = estateOwnershipDto.isPrimaryOwner
            };
        }
    }
}
