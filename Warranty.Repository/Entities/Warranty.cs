using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WarrantyManagement.Entities
{
    public class Warranty
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Creator { get; set; }
        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public DateTime? WarrantyDate { get; set; }

        public string Status { get; set; }

        public string? Sale { get; set; }

        public string? Technician { get; set; }
        public string? Receiver { get; set; }

        //many-to-many relationship with WarrantyDevice
        [JsonIgnore]
        public ICollection<WarrantyDevice> WarrantyDevices { get; set; }


        //many-to-one relationship with Customer
        public string CustomerId { get; set; }
        public User Customer { get; set; }

        [JsonIgnore]
        public ICollection<WarrantyHistory> WarrantyHistories { get; set; } = new List<WarrantyHistory>();
    }
}
