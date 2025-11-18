using Domain.DomainRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainRules.UserRules
{
    public class PasswordDelegateCheckAll
    {
        public delegate RuleResult PasswordRule(string password);

        public static RuleResult CheckAll(string password, params PasswordRule[] rules)
        {
            foreach (var rule in rules)
            {
                var result = rule(password);
                if (!result.IsAllowed)
                return result;
                
            }

            return RuleResult.Success();
        }
    }

}