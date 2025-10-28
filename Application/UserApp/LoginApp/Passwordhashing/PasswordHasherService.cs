using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.LoginApp.Passwordhashing
{
  public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool VerifyPassword(string hash, string password) => BCrypt.Net.BCrypt.Verify(password, hash);
}
}
