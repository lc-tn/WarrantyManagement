using WarrantyManagement.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAll();
        Task<Role> GetById(int id);
        Task<Role> GetByName(string name);
        Task<Role> CreateRole(Role role);
    }
}
