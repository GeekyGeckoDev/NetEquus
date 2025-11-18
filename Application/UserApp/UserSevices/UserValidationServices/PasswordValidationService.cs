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
    public class PasswordValidationService : IPasswordValidationService
    {
        public async Task<RuleResult> CheckPasswordAsync(string password)
        {
            return PasswordDelegateCheckAll.CheckAll(password,
                PasswordRules.PasswordCannotBeEmpty,
                PasswordRules.PasswordMinLength,
                PasswordRules.PasswordHasUppercase,
                PasswordRules.PasswordHasLowercase,
                PasswordRules.PasswordHasDigit);
        }

    }
}