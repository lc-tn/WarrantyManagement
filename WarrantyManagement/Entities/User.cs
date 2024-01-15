using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WarrantyManagement.Entities
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        //one-to-many relationship with Warranty
        public ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
    }
}
