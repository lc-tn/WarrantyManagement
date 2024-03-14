using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WarrantyManagement.Entities
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //many-to-many relationship with Role
        [JsonIgnore]
        public ICollection<RolePermission> RolePermissions { get; }
    }
}
