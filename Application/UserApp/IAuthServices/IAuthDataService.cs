using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Application.Responses;
namespace Application.UserApp.AuthServices
{
    public interface IAuthDataService
    {
        Task<ServiceResponse<ClaimsPrincipal>> Login(string email, string password);
    }
}
