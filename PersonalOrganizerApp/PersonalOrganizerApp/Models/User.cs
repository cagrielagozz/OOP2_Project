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

        public User Clone()
        {
            return new User
            {
                Username = this.Username,
                Name = this.Name,
                Surname = this.Surname,
                PhoneNumber = this.PhoneNumber,
                Address = this.Address,
                Email = this.Email,
                Password = this.Password,
                Base64Photo = this.Base64Photo
            };
        }

        public void CopyFrom(User other)
        {
            Username = other.Username;
            Name = other.Name;
            Surname = other.Surname;
            PhoneNumber = other.PhoneNumber;
            Address = other.Address;
            Email = other.Email;
            Password = other.Password;
            Base64Photo = other.Base64Photo;
        }
    }

}
