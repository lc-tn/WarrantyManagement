
using WarrantyManagement.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IDeviceRepository
    {
        bool Save();
        Task<List<Device>> GetAll();
        Task<Device> GetDeviceById(int id);
        Task<Device?> GetDeviceByName(string name);
        Task<List<Device>> GetDeviceByUser(string userId);
        Task<bool> Create(Device device);
        bool EditDevices(List<Device> devices);
    }
}
