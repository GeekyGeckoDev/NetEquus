using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedApp.IOwnershipRepos
{
    public interface IEstateOwnershipValidationRepository
    {
        Task<bool> UserAlreadyOwnsEstateAsync(Guid userId, Guid estateId);
    }
}
