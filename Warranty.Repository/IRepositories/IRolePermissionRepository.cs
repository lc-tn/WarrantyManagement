using WarrantyManagement.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IRolePermissionRepository
    {
        Task<bool> DeleteByRole(int roleId);
        Task<bool> Add(RolePermission rolePermission);
    }
}
