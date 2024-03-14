using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WarrantyManagement.Entities
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public string Status { get; set; }
        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }

        //many-to-one relationship with Warranty
        [JsonIgnore]
        public ICollection<WarrantyDevice> WarrantyDevices { get; set; }

        //many-to-many relationship with WarrantyDevice
        public ICollection<WarrantyHistory> WarrantyHistories { get; set; } = new List<WarrantyHistory>();

        //one-to-many relationship with User
        public string UserId { get; set; }
    }
}
