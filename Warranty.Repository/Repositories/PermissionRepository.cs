using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Entities;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public PermissionRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }
        public async Task<List<Permission>> GetByRoleId(int roleId)
        {
            var role = await _context.Roles.Include(r => r.RolePermissions).ThenInclude(rp => rp.Permission).SingleOrDefaultAsync(r => r.Id == roleId);
            return role.RolePermissions.Select(rp => rp.Permission).ToList();
        }

        public async Task<List<Permission>> GetAll()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission> GetById(int id)
        {
            return await _context.Permissions.SingleAsync(p => p.Id == id);
        }
    }
}
