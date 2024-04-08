using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Entities;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Repositories
{
    public class WarrantyHistoryRepository : IWarrantyHistoryRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public WarrantyHistoryRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Add(WarrantyHistory warrantyHistory)
        {
            var entry = _context.Add(warrantyHistory);
            if (entry.State == EntityState.Added)
            {
                Save();
                return true;
            }
            return false;
        }

        public async Task<List<WarrantyHistory>> GetByWarranty(int warrantyId, int pageNumber, int pageSize)
        {
            return await _context.WarrantyHistories.Where(w => w.WarrantyId==warrantyId)
                .OrderByDescending(w => w.ModifyDate)
                .Skip(pageNumber).Take(pageSize).ToListAsync();
        }

        public Task<int> Total(int warrantyId)
        {
            return _context.WarrantyHistories.Where(w => w.WarrantyId == warrantyId).CountAsync();
        }
    }
}
