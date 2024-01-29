using System.ComponentModel.DataAnnotations;

namespace WarrantyManagement.Entities
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public string Status { get; set; }

        //many-to-one relationship with Category
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        //many-to-one relationship with Warranty
        public ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
    }
}
