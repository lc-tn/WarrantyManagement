namespace WarrantyManagement.Model
{
    public class GetWarrantyHistoryModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string Status { get; set; }
        public string Sale { get; set; }
        public string Technician { get; set; }
        public string Modifier { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int WarrantyId { get; set; }
    }

    public class AddWarrantyHistoryModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string Status { get; set; }
        public string Sale { get; set; }
        public string Technician { get; set; }
        public string Modifier { get; set; }
        public int WarrantyId { get; set; }
    }
}
