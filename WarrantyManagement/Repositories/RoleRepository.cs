using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Entities;

namespace WarrantyManagement.Repositories
{
    public class RoleRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public RoleRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAll()
        {
            return _context.Roles.ToList();
        }

        public async Task<Role> GetById(int id)
        {
            return _context.Roles.Where(c => c.Id == id).SingleOrDefault();
        }

        public async Task<Role> GetByName(string name)
        {
            return _context.Roles.Where(c => c.Name.Equals(name)).SingleOrDefault();
        }

        public async Task<Role> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }
    }
}
