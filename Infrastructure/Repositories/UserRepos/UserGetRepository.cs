using Application.UserApp.IUserRepos;
using Domain.Entities.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UserRepos
{
    public class UserGetRepository : IUserGetRepository
    {
        private readonly NetEquusDbContext _context;

        public UserGetRepository(NetEquusDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
