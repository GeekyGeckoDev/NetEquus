using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainRules.EstateRules
{
    public class EstateRules
    {
        public static EstateDelegateCheckAll.EstateRule EstateNameCannotBeEmpty = static (estate) =>
        string.IsNullOrEmpty(estate.EstateName)
        ? RuleResult.Fail("Estate name cannot be empty")
        : RuleResult.Success();

        public static EstateDelegateCheckAll.EstateRule EstateNameAlreadyExists(Func<string, bool> existsFunc) =>
            (estate) => existsFunc(estate.EstateName)
            ? RuleResult.Fail("Estate name already exists")
            : RuleResult.Success();

    }


}