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
    public class EmailValidationService : IEmailValidationService
    {

        private readonly IUserValidationRepository _userValidationRepository;

        public EmailValidationService(IUserValidationRepository userValidationRepository)
        {
            _userValidationRepository = userValidationRepository;
        }

        public async Task<RuleResult> CheckEmailAsync(UserDto userDto)
        {
            bool exists = await _userValidationRepository.EmailExistsAsync(userDto.Email);

            return EmailDelegateCheckAll.CheckAll(userDto.Email,
                EmailRules.EmailCannotBeEmpty,
                EmailRules.EmailHasValidFormat,
                EmailRules.EmailAlreadyExists(_ => exists)
                );
        }


    }
}
