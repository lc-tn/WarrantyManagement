using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WarrantyManagement.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //many-to-many relationship with Permission
        [JsonIgnore]
        public ICollection<RolePermission> RolePermissions { get; set; }

        //many-to-one relationship with User
        public ICollection<User> Users { get; set; }
    }
}
