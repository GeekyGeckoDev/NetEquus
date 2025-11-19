using Application.UserApp.UserDtos;
using Domain.DomainRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.IUserServices.IUserValidationServices
{
    public interface IRegistrationValidationService
    {
        Task<RuleResult> CheckUsernameAsync(UserRegistrationDto userRegistrationDto);
    }
}
