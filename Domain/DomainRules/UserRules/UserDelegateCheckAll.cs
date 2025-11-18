using Domain.DomainRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainRules.UserRules
{
    public class UserDelegateCheckAll
    {
        public delegate RuleResult UsernameRule(string username);
        public static RuleResult CheckAll(string username, params UsernameRule[] rules)
        {
            foreach (var rule in rules)
            {
                var result = rule(username); // pass the bool directly
                if (!result.IsAllowed)
                    return result;
            }
            return RuleResult.Success();
        }

    }
}