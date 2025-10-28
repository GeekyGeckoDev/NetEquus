using Application.UserApp.LoginApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.LoginApp.IAuthenticationServices
{
    public interface IAuthDataService
    {
        ServiceResponse<ClaimsPrincipal> Login(string email, string password);


    }
}
