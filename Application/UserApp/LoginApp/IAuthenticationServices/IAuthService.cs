using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.LoginApp.IAuthenticationServices
{
    public interface IAuthService
    {
        Task Login(ClaimsPrincipal user);

        Task Logout();

        Task<bool> RestoreFromLocalStorage();
    }
}
