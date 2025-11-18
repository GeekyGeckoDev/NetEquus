using Application.UserApp.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.IUserServices
{
    public interface ILogInService
    {
        Task<UserDto?> LoginAsync(string username, string password);
    }
}
