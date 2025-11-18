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
    public class UserGetService : IUserGetService
    {
        private readonly IUserGetRepository _userGetRepository;

        public UserGetService(IUserGetRepository userGetRepository)
        {
            _userGetRepository = userGetRepository;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userGetRepository.GetUserByUsernameAsync(username);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userGetRepository.GetUserByEmailAsync(email);
        }
    }
}
