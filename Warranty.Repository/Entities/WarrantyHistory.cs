using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarrantyManagement.Entities
{
    public class WarrantyHistory
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string Status { get; set; }
        public string? Sale { get; set; }
        public string? Technician { get; set; }
        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }

        //one-to-many relationship with warranty
        public int WarrantyId { get; set; }
        public Warranty Warranty { get; set; }
    }
}
