using WarrantyManagement.Entities;

namespace WarrantyManagement.Model
{
    public class WarrantyModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string Status { get; set; }

        public int DeviceId { get; set; }

        public string CustomerId { get; set; }

    }
}
