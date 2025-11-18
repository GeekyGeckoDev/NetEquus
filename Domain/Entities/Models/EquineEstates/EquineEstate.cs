using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.EquineEstates
{
    public class EquineEstate
    {
        [Key]
        public Guid EstateId { get; set; }

        [NotMapped]
        public string _estatename { get; set; }
        public string EstateName 
        {
            get => _estatename;
            set 
            { _estatename = value;
                NormalizedEstateName = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : new string(value
                    .ToLowerInvariant()
                    .Where(c => char.IsLetterOrDigit(c))
                    .ToArray());
            } 
        }

        public string NormalizedEstateName { get; set; }

        public string EstateDescription { get; set; }

        public int HorseCapacity { get; set; }

        public int CurrentBalance { get; set; }

        public bool IsSytemEstate { get; set; }

        public virtual ICollection<EstateOwner> EstateOwners { get; set; } = new List<EstateOwner>();

        public EquineEstate (string estateName, bool  isSytemEstate)
        {
            EstateId = Guid.NewGuid ();
            EstateName = estateName;
            IsSytemEstate = isSytemEstate;
        }
        public EquineEstate()
        {

        }
    }
}
