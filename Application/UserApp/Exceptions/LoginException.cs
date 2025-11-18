using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message) { }
    }
}
