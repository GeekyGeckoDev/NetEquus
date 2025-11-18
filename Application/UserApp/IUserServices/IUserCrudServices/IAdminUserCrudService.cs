using Domain.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.IUserServices.IUserCrudServices
{
    public interface IAdminUserCrudService
    {
        Task CreateUserAsync(User user);

        Task DeleteUserAsync(User user);

        Task UpdateUserAsync(User existingUser, User updatedUser);
    }
}
