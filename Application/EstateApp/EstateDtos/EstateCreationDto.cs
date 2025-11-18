using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.EstateDtos
{
    public class EstateCreationDto
    {
        public string EstateName { get; set; }

        public int StartBalance { get; set; } = 50000;

        public int StartHorseCapacity { get; set; } = 10;

        public string Description { get; set; } = "Here you can write a little about yourself";

    }
}
