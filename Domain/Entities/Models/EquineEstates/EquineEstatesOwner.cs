using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.EquineEstates
{
    public class EquineEstatesOwner
    {
        public Guid EstateOwnerId { get; set; }
        public Guid UserId { get; set; }
        public Guid EstateId { get; set; }

        public virtual User User { get; set; }
        public virtual EquineEstate Estate { get; set; }
    }
}
