using Application.EstateApp.IEstateRepos;
using Domain.Entities.Models.EquineEstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EstateRepos
{
    public class EstateCrudRepository : IEstateCrudRepository
    {
        private readonly NetEquusDbContext _context;

        public EstateCrudRepository (NetEquusDbContext context)
        {
            _context = context;
        }

        public async Task CreateEstateAsync(EquineEstate newEstate)
        {
            await _context.EquineEstates.AddAsync(newEstate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEstateAsync (EquineEstate equineEstate)
        {
            _context.EquineEstates.Update(equineEstate);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEstateAsync (EquineEstate equineEstate)
        {
            _context.EquineEstates.Remove(equineEstate);
            await _context.SaveChangesAsync();
        }

    }
}
