using Application.UserApp.IUserServices.IUserCrudServices;
using Application.UserApp.PasswordHasherServices;
using Application.UserApp.UserDtos;
using Application.UserApp.UserMappers;
using Application.UserApp.Exceptions;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UserApp.IUserServices;

namespace Application.UserApp.UserSevices.UserManagerServices
{
    public class LogInService : ILogInService
    {
        private readonly IUserGetService _userGetService;

        private readonly IPasswordHasherService _passwordHasherService;

        public LogInService(IUserGetService userGetService, IPasswordHasherService passwordHasherService)
        {
            _userGetService = userGetService;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<UserDto?> ValidateUserAsync(string username, string password)
        {
            var user = await _userGetService.GetUserByEmailAsync(username);

            if (user == null || !_passwordHasherService.VerifyPassword(password, user.PasswordHash))
                throw new LoginException("Username or password is incorrect");

            return UserMapper.ToDto(user);
        }
    }
}
