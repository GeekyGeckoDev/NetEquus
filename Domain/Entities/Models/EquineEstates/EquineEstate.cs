using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.EquineEstates
{
    public class EquineEstate
    {
        [Key]
        public Guid EstateId { get; set; }

        public string EstateName { get; set; }

        public string EstateDescription { get; set; }

        public int HorseCapacity { get; set; }

        public int CurrentBalance { get; set; }

        public bool IsSytemEstate { get; set; }

        public virtual ICollection<EquineEstatesOwner> EstateOwners { get; set; } = new List<EquineEstatesOwner>();

        public EquineEstate(string estateName, bool isSytemEstate)
        {
            EstateId = Guid.NewGuid();
            EstateName = estateName;
            IsSytemEstate = isSytemEstate;
        }
        public EquineEstate()
        {

        }
    }
}
