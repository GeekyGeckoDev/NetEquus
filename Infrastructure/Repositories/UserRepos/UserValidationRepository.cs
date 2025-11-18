using Application.UserApp.IUserRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UserRepos
{
    public class UserValidationRepository : IUserValidationRepository
    {
        private readonly NetEquusDbContext _context;

        public UserValidationRepository(NetEquusDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            var normalized = username.ToLowerInvariant();
            return await _context.Users
                .AnyAsync(u => (u.NormalizedUsername ?? string.Empty) == normalized);
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
