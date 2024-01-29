using System.ComponentModel.DataAnnotations;

namespace WarrantyManagement.Entities
{
    public class Warranty
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        //public DateTime ApointmentDate { get; set; }

        public string Status { get; set; }

        //one-to-many relationship with device
        public int DeviceId { get; set; }
        public Device Device { get; set; }

        //many-to-one relationship with Customer
        public string CustomerId { get; set; }
        public User Customer { get; set; }
    }
}
