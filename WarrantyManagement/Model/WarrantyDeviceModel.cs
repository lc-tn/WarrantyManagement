namespace WarrantyManagement.Model
{
    public class WarrantyDeviceModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; }
        public string? Result { get; set; }
        public string Description { get; set; }
    }

    public class EditWarrantyDeviceModel
    {
        public int WarrantyId { get; set; }
        public int DeviceId { get; set; }
        public string Status { get; set; }
        public string? Result { get; set; }
        public string Description { get; set; }
        public string Modifier { get; set; }
    }
}
