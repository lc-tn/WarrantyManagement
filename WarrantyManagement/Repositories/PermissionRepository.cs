using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Entities;

namespace WarrantyManagement.Repositories
{
    public class PermissionRepository
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
            return _context.Permissions.ToList();
        }

        public async Task<Permission> GetById(int id)
        {
            return _context.Permissions.Where(p => p.Id == id).SingleOrDefault();
        }
    }
}
