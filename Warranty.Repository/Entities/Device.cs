using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WarrantyRepository.Entities;

namespace WarrantyManagement.Entities
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? ActivationDate { get; set; }
        public DateTime? WarrantyPeriod { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ReplacementDevice { get; set; }
        public string? UserId { get; set; }

        //many-to-one relationship with Warranty
        [JsonIgnore]
        public ICollection<WarrantyDevice> WarrantyDevices { get; set; }

        //many-to-many relationship with WarrantyDevice
        public ICollection<WarrantyHistory> WarrantyHistories { get; set; } = new List<WarrantyHistory>();

        //one-to-many relationship with Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
