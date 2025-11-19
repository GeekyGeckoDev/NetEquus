using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UserApp.UserDtos;
using Domain.Entities.Models.Users;

namespace Application.UserApp.UserMappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = null,
                IsAdmin = user.IsAdmin
            };
        }

        public static User ToUser(UserDto userDto)
        {
            return new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                IsAdmin = userDto.IsAdmin

            };
        }

        public static User ToNewUser(UserRegistrationDto userRegistrationDto)
        {
            return new User
            {
                Username = userRegistrationDto.Username,
                Email = userRegistrationDto.Email

            };
        }

    }
}
