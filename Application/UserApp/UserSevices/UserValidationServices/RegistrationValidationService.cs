using Application.UserApp.IUserRepos;
using Application.UserApp.IUserServices.IUserValidationServices;
using Application.UserApp.UserDtos;
using Domain.DomainRules;
using Domain.DomainRules.UserRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.UserSevices.UserValidationServices
{
    public class RegistrationValidationService : IRegistrationValidationService
    {
        private readonly IUserValidationRepository _userValidationRepository;

        public RegistrationValidationService(IUserValidationRepository userValidationRepository)
        {
            _userValidationRepository = userValidationRepository;
        }


        public async Task<RuleResult> CheckUsernameAsync(UserRegistrationDto userRegistrationDto)
        {
            // 1️⃣ Ask the repository if username exists
            bool exists = await _userValidationRepository.UsernameExistsAsync(userRegistrationDto.Username);

            // 2️⃣ Apply domain rules via delegate
            return UserDelegateCheckAll.CheckAll(userRegistrationDto.Username,
                UsernameRules.UsernameCannotByEmpty,
                UsernameRules.UsernameAlreadyExists( _ => exists));
        }


    }
}