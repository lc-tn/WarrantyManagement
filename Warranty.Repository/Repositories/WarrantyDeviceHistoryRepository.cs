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

        public List<WarrantyDeviceHistory> GetDeviceByWarrantyId(int warrantyId, int pageNumber, int pageSize)
        {
            return _context.WarrantyDeviceHistories.Where(wd => wd.WarrantyId == warrantyId).OrderByDescending(w => w.ModifyDate)
                .Skip(pageNumber).Take(pageSize).ToList();
        }

        public WarrantyDeviceHistory GetWarrantyDeviceHistory(int warrantyId, int deviceId)
        {
            var r = _context.WarrantyDeviceHistories.Where(wd => wd.WarrantyId == warrantyId &&
                               wd.DeviceId == deviceId).OrderByDescending(w => w.ModifyDate);
            return _context.WarrantyDeviceHistories.Where(wd => wd.WarrantyId == warrantyId && 
                               wd.DeviceId == deviceId).OrderByDescending(w => w.ModifyDate).FirstOrDefault();
        }

        public Task<int> Total(int warrantyId)
        {
            return _context.WarrantyDeviceHistories.Where(w => w.WarrantyId == warrantyId).CountAsync();
        }
    }
}
