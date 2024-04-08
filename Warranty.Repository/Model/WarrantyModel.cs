
namespace WarrantyManagement.Model
{
    public class CreateWarrantyModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string Creator { get; set; }
        public List<DeviceWarrantyModel> Device { get; set; }
        public string CustomerId { get; set; }
    }

    public class GetWarrantyByIdModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Creator {  get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string Status { get; set; }
        public string? Sale { get; set; }
        public string? Technician { get; set; }
        public string? SaleId { get; set; }
        public string? TechnicianId { get; set; }
        public List<WarrantyDeviceModel> Device { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
    }

    public class EditWarrantyModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Modifier { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string Status { get; set; }
        public string? Sale { get; set; }
        public string? Technician { get; set; }
    }

    public class WarrantyModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
    }
}
