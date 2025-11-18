using Domain.DomainRules;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.DomainRules.UserRules.EmailDelegateCheckAll;

namespace Domain.DomainRules.SharedRules
{
    public class EstateOwnershipDelegateCheckAll
    {
        public delegate RuleResult EstateOwnershipRule (EstateOwner estateOwner);

        public static RuleResult CheckAll (EstateOwner estateOwner, params EstateOwnershipRule[] rules)
        {
            foreach (var rule in rules)
            {
                var result = rule(estateOwner);
                if (!result.IsAllowed)
                    return result;
            }

            return RuleResult.Success();
        }

    }
}