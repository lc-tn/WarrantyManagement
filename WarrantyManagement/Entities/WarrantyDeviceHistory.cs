using System.ComponentModel.DataAnnotations;

namespace WarrantyManagement.Entities
{
    public class WarrantyDeviceHistory
    {
        [Key]
        public int Id { get; set; }
        public int WarrantyId { get; set; }
        public int DeviceId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string? Result { get; set; }
        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
