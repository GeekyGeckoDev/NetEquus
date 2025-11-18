using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Application.UserApp.UserDtos;
using Application.UserApp.UserMappers;

namespace Application.UserApp.PasswordHasherServices
{


        public class PasswordHasherService : IPasswordHasherService
        {
            public string HashPassword(string plainPassword)
            {
                return BCrypt.Net.BCrypt.HashPassword(plainPassword);
            }

            public bool VerifyPassword(string plainPassword, string hashedPassword)
            {
                return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
            }
        }
    
}