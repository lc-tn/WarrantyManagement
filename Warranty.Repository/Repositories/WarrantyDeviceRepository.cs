using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Entities;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Repositories
{
    public class WarrantyDeviceRepository : IWarrantyDeviceRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public WarrantyDeviceRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Add(WarrantyDevice warrantyDevice)
        {
            var entry = _context.WarrantyDevices.Add(warrantyDevice);
            if (entry.State == EntityState.Added)
            {
                if (Save())
                    return true;
            }
            return false;
        }

        public List<WarrantyDevice> GetDeviceByWarrantyId(int warrantyId)
        {
            return _context.WarrantyDevices.Where(wd => wd.WarrantyId == warrantyId).ToList();
        }

        public WarrantyDevice GetWarrantyDevice(int warrantyId, int deviceId)
        {
            return _context.WarrantyDevices.Single(wd => wd.WarrantyId == warrantyId && wd.DeviceId == deviceId);
        }

        public bool Edit(WarrantyDevice warrantyDevice)
        {
            _context.WarrantyDevices.Update(warrantyDevice);
            Save();
            return true;
        }
    }
}