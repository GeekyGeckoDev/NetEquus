using Domain.DomainRules;
using Domain.DomainRules.UserRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainRules.UserRules
{
    public class UsernameRules
    {
       

        public static UserDelegateCheckAll.UsernameRule UsernameCannotByEmpty = static (username) =>
        string.IsNullOrWhiteSpace(username)
        ? RuleResult.Fail("Username cannot be empty")
        : RuleResult.Success();
        



        public static UserDelegateCheckAll.UsernameRule UsernameAlreadyExists(Func<string, bool> existsFunc) =>
        (username) => existsFunc(username)
        ? RuleResult.Fail("Username already exists")
        : RuleResult.Success();
    }
}