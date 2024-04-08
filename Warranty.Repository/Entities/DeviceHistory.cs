using System.ComponentModel.DataAnnotations;
using WarrantyManagement.Entities;

namespace WarrantyRepository.Entities
{
    public class DeviceHistory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ReplacementDevice { get; set; }
        public string? UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
