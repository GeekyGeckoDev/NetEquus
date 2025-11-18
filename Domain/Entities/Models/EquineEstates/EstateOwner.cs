using Domain.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.EquineEstates
{
    public class EstateOwner
    {
        [Key]
        public int OwnershipId { get; set; }

        public Guid EstateId { get; set; }

        public Guid UserId { get; set; }

        public bool IsPrimaryOwner { get; set; }

        [ForeignKey("EstateId")]

        public virtual EquineEstate EquineEstate { get; set; }

        [ForeignKey("UserId")]

        public virtual User User { get; set; }
    }
}
