using Application.UserApp.IUserRepos;
using Application.UserApp.IUserServices.IUserCrudServices;
using Domain.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.UserSevices.UserCrudServices
{
    public class UserCrudService
    {
        protected readonly IUserCrudRepository _userCrudRepository;

        public UserCrudService(IUserCrudRepository userCrudRepository)
        {
            _userCrudRepository = userCrudRepository;
        }

        // Make it virtual so child classes can override it
        public virtual async Task UpdateUserAsync(User existingUser, User updatedUser)
        {
            // Default implementation (optional)
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;

            await _userCrudRepository.UpdateUserAsync(existingUser);
        }
    }

    public class Admin : UserCrudService, IAdminUserCrudService
    {
        public Admin(IUserCrudRepository userCrudRepository) : base(userCrudRepository)
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

        // Override parent method
        public override async Task UpdateUserAsync(User existingUser, User updatedUser)
        {
            // Custom admin-specific update logic (if any)
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;

            await _userCrudRepository.UpdateUserAsync(existingUser);
        }
    }

    public class Client : UserCrudService, IClientUserCrudService
    {
        public Client (IUserCrudRepository userCrudRepository) : base(userCrudRepository)
        {
        }

        public override async Task UpdateUserAsync(User existingUser, User updatedUser)
        {
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;

            await _userCrudRepository.UpdateUserAsync(existingUser);
        }

    }
}
