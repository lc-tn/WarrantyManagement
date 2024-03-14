
namespace WarrantyManagement.Entities
{
    public class WarrantyDevice
    {
        //public int Id { get; set; }

        public int WarrantyId { get; set; }
        public Warranty Warranty { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string? Result { get; set; }

        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}