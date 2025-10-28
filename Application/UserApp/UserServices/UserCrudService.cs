using Application.UserApp.IUserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.UserServices
{
    public class UserCrudService : IUserCrudService
    {
        protected readonly IUserCrudRepository _userCrudRepository;

        public UserCrudService(IUserCrudRepository userCrudRepository)
        {
            _userCrudRepository = userCrudRepository;
        }

        public virtual async Task UpdateUserAsync(User existingUser, User updatedUser)
        {
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;

            await _userCrudRepository.UpdateUserAsync(existingUser);
        }
    }

    public class AdminUserCrudService : UserCrudService, IAdminUserCrudService
    {
        public AdminUserCrudService(IUserCrudRepository userCrudRepository)
            : base(userCrudRepository)
        {
        }

        public async Task CreateUserAsync(User user)
        {
            await _userCrudRepository.CreateUserAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await _userCrudRepository.DeleteUserAsync(user);
        }
    }

    public class ClientUserCrudService : UserCrudService, IClientUserCrudService
}
