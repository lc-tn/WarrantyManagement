namespace WarrantyManagement.Model
{ 
    public class DeviceWarrantyModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class DeviceModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string UserId {  get; set; }
        public string UserName { get; set; }
        public int? ReplacementDevice { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? WarrantyPeriod { get; set; }
    }

    public class ReplacementDeviceModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ReplacementDevice { get; set; }
        public string Modifier { get; set; }
    }

    public enum DeviceStatus
    {
        USING,
        FIXING,
        REPLACING
    }
}
