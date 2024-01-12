using System.ComponentModel.DataAnnotations;

namespace WarrantyManagement.Entities
{
    public class Customer
    {
        [Required]
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }

        //one-to-many relationship with Warranty
        public ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
    }
}
