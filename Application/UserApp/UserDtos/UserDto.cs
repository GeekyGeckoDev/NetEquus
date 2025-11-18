using Domain.DomainRules.UserRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserApp.UserDtos
{
    public class UserDto
    {

        public Guid UserId { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }
}
