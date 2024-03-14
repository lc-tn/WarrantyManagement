using Microsoft.EntityFrameworkCore;
using WarrantyManagement.Entities;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Repositories
{
    public class DeviceRepository :IDeviceRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public DeviceRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<List<Device>> GetAll()
        {
            return await _context.Devices.ToListAsync();
        }

        public async Task<Device> GetDeviceById(int id)
        {
            return await _context.Devices.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Device?> GetDeviceByName(string name) 
        {
            return await _context.Devices.Where(c => c.Name.Equals(name)).SingleOrDefaultAsync();
        }

        public async Task<List<Device>> GetDeviceByUser(string userId)
        {
            return await _context.Devices.Where(c => c.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<bool> Create(Device device)
        {
            var entry = await _context.AddAsync(device);
            if (entry.State == EntityState.Added)
            {
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditDevices (List<Device> devices)
        {
             _context.UpdateRange(devices);
            return true;
        }
    }
}
