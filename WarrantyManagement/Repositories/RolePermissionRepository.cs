using Microsoft.EntityFrameworkCore;
using System;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;

namespace WarrantyManagement.Repositories
{
    public class RolePermissionRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public RolePermissionRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteByRole(int roleId)
        {
            var rolePermission = await _context.RolePermissions.Where(rp => rp.RoleId == roleId).ToListAsync();
            if (rolePermission != null)
            {
                _context.RolePermissions.RemoveRange(rolePermission);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Add(RolePermission rolePermission)
        {
            await _context.AddAsync(rolePermission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
