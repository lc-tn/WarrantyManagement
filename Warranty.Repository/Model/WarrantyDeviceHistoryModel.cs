namespace WarrantyManagement.Model
{
    public class WarrantyDeviceHistoryModel
    {
        public int WarrantyId { get; set; }
        public int DeviceId { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public string? Result { get; set; }
        public string? Reason { get; set; }
        public string? Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? ReplacementDevice { get; set; }
        public string? ReplacementDeviceName { get; set; }
    }
}
