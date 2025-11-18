using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Models.Users
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        private string _username;
        public string Username {
            get => _username;
            set
            {
                _username = value;
                NormalizedUsername = value?.ToLower();
            } 
        }

        public string NormalizedUsername { get; set; }
        

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = true;

        public User(string name, string email, string passwordHash, bool isAdmin)
        {
            UserId = Guid.NewGuid();

            Username = name;

            Email = email;

            PasswordHash = passwordHash;

            IsAdmin = isAdmin;
        }

        public User()
        {
        }
    }
}
