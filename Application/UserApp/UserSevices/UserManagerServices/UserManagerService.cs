using Application.UserApp.IUserServices;
using Application.UserApp.IUserServices.IUserCrudServices;
using Application.UserApp.PasswordHasherServices;
using Application.UserApp.IUserServices.IUserValidationServices;
using Application.UserApp.UserDtos;
using Application.UserApp.UserMappers;
using Application.UserApp.IUserServices.IUserValidationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DomainRules;

namespace Application.UserApp.UserSevices.UserManagerServices
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IAdminUserCrudService _adminUserCrudService;

        private readonly IPasswordHasherService _passwordHasherService;

        private readonly IRegistrationValidationService _registrationValidationService;

        private readonly IPasswordValidationService _passwordValidationService;


        public UserManagerService(IAdminUserCrudService adminUserCrudService, IPasswordHasherService passwordHasherService, IRegistrationValidationService registrationValidationService, IPasswordValidationService passwordValidationService)
        {
            _adminUserCrudService = adminUserCrudService;
            _passwordHasherService = passwordHasherService;
            _registrationValidationService = registrationValidationService;
            _passwordValidationService = passwordValidationService;
        }

        public async Task<RuleResult> HashAndCreateUserAsync(UserRegistrationDto userRegistrationDto)
        {

            var usernameCheck = await _registrationValidationService.CheckUsernameAsync(userRegistrationDto);
            if (usernameCheck == null || !usernameCheck.IsAllowed)
                return usernameCheck;

            var passwordCheck = await _passwordValidationService.CheckPasswordAsync(userRegistrationDto.Password);
            if (!passwordCheck.IsAllowed)
                return passwordCheck;

            var user = UserMapper.ToNewUser(userRegistrationDto);

            user.PasswordHash = _passwordHasherService.HashPassword(userRegistrationDto.Password);

            await _adminUserCrudService.CreateUserAsync(user);


            return RuleResult.Success();
        }
    }
}
