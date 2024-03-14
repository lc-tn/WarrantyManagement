using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Entities;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public RoleRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetById(int id)
        {
            return await _context.Roles.SingleAsync(c => c.Id == id);
        }

        public async Task<Role> GetByName(string name)
        {
            return await _context.Roles.SingleAsync(c => c.Name.Equals(name));
        }

        public async Task<Role> CreateRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            _context.SaveChanges();
            return role;
        }
    }
}
