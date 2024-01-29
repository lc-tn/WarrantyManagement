using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WarrantyManagement.Entities
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        //one-to-many relationship with Role
        public int RoleId { get; set; }
        public Role Role { get; set; }

        //one-to-many relationship with Warranty
        [JsonIgnore]
        public ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
    }
}
