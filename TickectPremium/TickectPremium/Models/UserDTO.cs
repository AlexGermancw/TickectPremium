using System;
using System.Collections.Generic;
using System.Text;

namespace TickectPremium.Models
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public bool IsNullOrEmpty()
        {
            if (this == null)
                return true;

            if (string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(Phone))
            {
                return true;
            }

            return false;
        }
    }
}
