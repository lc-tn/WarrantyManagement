using WarrantyManagement.Entities;

namespace WarrantyManagement.Model
{
    public class WarrantyModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        //public DateTime ApointmentDate { get; set; }

        public string Status { get; set; }

        public string DeviceName { get; set; }

        public string CustomerName { get; set; }

    }
}
