using Domain.DomainRules.EstateRules;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainRules.SharedRules
{
    public class EstateOwnershipRules
    {
        public static EstateOwnershipDelegateCheckAll.EstateOwnershipRule UserCanOwnOnlyOneEstate(Func<Guid, bool> ownsAnyEstate)
        {
            return EstateOwner => ownsAnyEstate(EstateOwner.UserId)
                ? RuleResult.Fail("User already owns an estate")
                : RuleResult.Success();
        }
    }
}
