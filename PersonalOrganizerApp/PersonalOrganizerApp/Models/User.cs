using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalOrganizerApp.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Base64Photo { get; set; }
        public bool RememberMe { get; set; }
        public string UserTypes { get; set; }

        // Add the IsValid method to resolve the CS1061 error  
        public bool IsValid(string username, string password, bool rememberMe)
        {
            return this.Username == username && this.Password == password;
        }

        public User Clone()
        {
            return (User)this.MemberwiseClone();
        }

        public void CopyFrom(User other)
        {
            this.Username = other.Username;
            this.Name = other.Name;
            this.Surname = other.Surname;
            this.PhoneNumber = other.PhoneNumber;
            this.Address = other.Address;
            this.Email = other.Email;
            this.Password = other.Password;
            this.Base64Photo = other.Base64Photo;
            this.RememberMe = other.RememberMe;
            this.UserTypes = other.UserTypes;
        }
    }

}
