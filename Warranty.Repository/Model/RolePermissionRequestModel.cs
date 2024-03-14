using WarrantyManagement.Entities;

namespace WarrantyManagement.Model
{
    public class RolePermissionRequestModel
    {
        public int RoleId { get; set; }
        public List<int> PermissionId { get; set; }
    }
}
