using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.LoginApp.Passwordhashing
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        bool VerifyPassword(string hash, string password) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
