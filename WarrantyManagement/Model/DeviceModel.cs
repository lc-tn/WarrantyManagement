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
    }
}
