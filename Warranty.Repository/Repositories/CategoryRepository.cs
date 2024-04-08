using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Entities;
using WarrantyManagement.Repositories;
using WarrantyRepository.Entities;
using WarrantyRepository.IRepositories;

namespace WarrantyRepository.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public CategoryRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
