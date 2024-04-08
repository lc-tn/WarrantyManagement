using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Repositories;
using WarrantyRepository.Entities;
using WarrantyRepository.IRepositories;

namespace WarrantyRepository.Repositories
{
    public class DeviceHistoryRepository : IDeviceHistoryRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public DeviceHistoryRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(DeviceHistory deviceHistory)
        {
            var entry = await _context.AddAsync(deviceHistory);
            if (entry.State == EntityState.Added)
            {
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
