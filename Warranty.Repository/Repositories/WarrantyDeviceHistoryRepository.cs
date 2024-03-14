using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Entities;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Repositories
{
    public class WarrantyDeviceHistoryRepository : IWarrantyDeviceHistoryRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public WarrantyDeviceHistoryRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Add(WarrantyDeviceHistory warrantyDeviceHistory)
        {
            var entry = _context.WarrantyDeviceHistories.Add(warrantyDeviceHistory);
            if (entry.State == EntityState.Added)
            {
                Save();
                return true;
            }
            return false;
        }

        public List<WarrantyDeviceHistory> GetDeviceByWarrantyId(int warrantyId, int pageNumber)
        {
            return _context.WarrantyDeviceHistories.Where(wd => wd.WarrantyId == warrantyId).OrderByDescending(w => w.ModifyDate)
                .Skip(pageNumber).Take(3).ToList();
        }

        public Task<int> Total(int warrantyId)
        {
            return _context.WarrantyDeviceHistories.Where(w => w.WarrantyId == warrantyId).CountAsync();
        }
    }
}
