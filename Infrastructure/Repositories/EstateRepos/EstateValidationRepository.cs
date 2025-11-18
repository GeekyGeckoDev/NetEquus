using Application.EstateApp.IEstateRepos;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EstateRepos
{
    public class EstateValidationRepository : IEstateValidationRepository
    {
        private readonly NetEquusDbContext _context;

        public EstateValidationRepository(NetEquusDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EstateNameExistsAsync(string estateName)
        {
            if (string.IsNullOrWhiteSpace(estateName))
                return false;

            // Normalize input
            var normalized = new string(estateName
                .ToLowerInvariant()
                .Where(c => char.IsLetterOrDigit(c))
                .ToArray());

            return await _context.EquineEstates
                .AnyAsync(e => e.NormalizedEstateName == normalized);
        }
    }
}