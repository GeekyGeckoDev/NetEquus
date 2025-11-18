using Domain.DomainRules;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.DomainRules.UserRules.EmailDelegateCheckAll;

namespace Domain.DomainRules.EstateRules
{
    public class EstateDelegateCheckAll
    {
        public delegate RuleResult EstateRule(EquineEstate equineEstate);

        public static RuleResult CheckAll(EquineEstate equineEstate, params EstateRule[] rules)
        {
            foreach (var rule in rules)
            {
                var result = rule(equineEstate);
                if(!result.IsAllowed)
                    return result;
            }

            return RuleResult.Success();
        }

    }
}