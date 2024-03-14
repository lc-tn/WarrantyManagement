using WarrantyManagement.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetByRoleId(int roleId);
        Task<List<Permission>> GetAll();
        Task<Permission> GetById(int id);
    }
}
