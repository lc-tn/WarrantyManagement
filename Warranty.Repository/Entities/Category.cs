using System.ComponentModel.DataAnnotations;
using WarrantyManagement.Entities;

namespace WarrantyRepository.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //many-to-many relationship with Device
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}