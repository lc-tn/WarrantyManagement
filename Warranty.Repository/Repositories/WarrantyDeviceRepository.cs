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

        public bool Edit(WarrantyDevice warrantyDevice)
        {
            var entry = _context.WarrantyDevices.Update(warrantyDevice);
                Save();
            return true;
        }
    }
}