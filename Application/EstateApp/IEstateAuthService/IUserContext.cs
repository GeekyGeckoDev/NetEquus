using Application.EstateApp.EstateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EstateApp.IEstateAuthService
{
    public interface IUserContext
    {
        Task InitializeAsync();

        EstateDto? Estate { get; }
    }
}
