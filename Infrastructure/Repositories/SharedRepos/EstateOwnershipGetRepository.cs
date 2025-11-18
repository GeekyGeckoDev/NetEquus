using Domain.Entities.Models.EquineEstates;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SharedRepos
{
    public class EstateOwnershipGetRepository : IEstateOwnershipGetRepository
    {
        private readonly NetEquusDbContext _context;

        public EstateOwnershipGetRepository(NetEquusDbContext context)
        {
            _context = context;
        }

        public async Task<EquineEstate?> GetEstateOwnershipByUserId (Guid userId)
        {
            return await _context.EquineEstates
            .Include(e => e.EstateOwners) // so you can actually see the owners
            .FirstOrDefaultAsync(e => e.EstateOwners.Any(owner => owner.UserId == userId));

        }
    }
}
