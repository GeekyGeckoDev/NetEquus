using Application.SharedApp.IOwnershipRepos;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SharedRepos
{
    public class EstateOwnershipCrudRepository : IEstateOwnersipCrudRepository
    {
        private readonly NetEquusDbContext _context;

        public EstateOwnershipCrudRepository(NetEquusDbContext context)
        {
            _context = context;
        }

        public async Task CreateEstateOwnershipAsync (EstateOwner estateOwnership)
        {
            await _context.EstateOwners.AddAsync(estateOwnership);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateEstateOwnershipAsync (EstateOwner estateOwnership)
        {
            _context.EstateOwners.Update(estateOwnership);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEstateOwnershipAsync (EstateOwner estateOwnership)
        {
            _context.EstateOwners.Remove(estateOwnership);
            await _context.SaveChangesAsync();
        }

    }
}
