using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateDtos
{
    public class EstateDto
    {
        public Guid EstateId { get; set; }
        public string EstateName { get; set; }

        public string EstateDesciption { get; set; }

        public int HorseCapacity { get; set; }

        public int CurrentBalance { get; set; }

        public bool IsSytemEstate { get; set; }

        //public List<EstateOwnerDto> EstateOwners { get; set; } = new();
    }
}
