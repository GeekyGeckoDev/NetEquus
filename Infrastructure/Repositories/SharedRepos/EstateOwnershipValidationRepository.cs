using Application.SharedApp.IOwnershipRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SharedRepos
{
    public class EstateOwnershipValidationRepository : IEstateOwnershipValidationRepository
    {
        private readonly NetEquusDbContext _context;

        public EstateOwnershipValidationRepository(NetEquusDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserAlreadyOwnsEstateAsync (Guid userId, Guid estateId)
        {
            return await _context.EquineEstates
                .Where(e => e.EstateId == estateId)
                .SelectMany(e => e.EstateOwners)
                .AnyAsync(eeo => eeo.UserId == userId);
        }
    }
}
