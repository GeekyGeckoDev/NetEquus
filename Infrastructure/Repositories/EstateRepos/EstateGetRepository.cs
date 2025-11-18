using Domain.Entities.Models.EquineEstates;
using Application.EstateApp.IEstateRepos;
using Domain.Entities.Models.Users;
using Infrastructure;
using Infrastructure.Repositories.UserRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EstateRepos
{
    public class EstateGetRepository : IEstateGetRepository
    {
        private readonly NetEquusDbContext _context;

        public EstateGetRepository(NetEquusDbContext context)
        {
            _context = context;
        }

        public async Task<EquineEstate?> GetEstateByIdAsync (Guid estateId)
        {
            return await _context.EquineEstates
                .FirstOrDefaultAsync(ee => ee.EstateId == estateId);
        }
    }
}