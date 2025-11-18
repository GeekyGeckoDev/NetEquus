using Domain.Entities.Models.Users;
using Application.UserApp.IUserRepos;

namespace Infrastructure.Repositories.UserRepos
{
    public class UserCrudRepository : IUserCrudRepository
    {
        private readonly NetEquusDbContext _context;

        public UserCrudRepository(NetEquusDbContext context)
        {
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
