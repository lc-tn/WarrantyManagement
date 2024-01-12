using System.ComponentModel.DataAnnotations;

namespace WarrantyManagement.Entities
{
    public class Warranty
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string Status { get; set; }

        //one-to-many relationship with device
        public int DeviceId { get; set; }
        public Device Device { get; set; } = null!;

        //many-to-one relationship with Customer
        public string CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
