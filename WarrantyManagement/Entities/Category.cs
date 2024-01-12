using System.ComponentModel.DataAnnotations;

namespace WarrantyManagement.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //one-to-many relationship with Device
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
