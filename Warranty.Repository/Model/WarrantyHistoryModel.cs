
namespace WarrantyManagement.Model
{
    public class WarrantyHistoryModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string Status { get; set; }
        public string? Sale { get; set; }
        public string? Technician { get; set; }
        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
