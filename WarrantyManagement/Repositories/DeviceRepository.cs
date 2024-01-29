using WarrantyManagement.Entities;

namespace WarrantyManagement.Repositories
{
    public class DeviceRepository
    {
        private readonly WarrantyManagementDbContext _context;

        public DeviceRepository(WarrantyManagementDbContext context)
        {
            _context = context;
        }

        public async Task<Device> GetDeviceByIdAsnc(int id)
        {
            return _context.Devices.Where(c => c.Id == id).SingleOrDefault();
        }

        public async Task<Device> GetDeviceByNameAsnc(string name) 
        {
            return _context.Devices.Where(c => c.Name.Equals(name)).SingleOrDefault();
        }
    }
}
