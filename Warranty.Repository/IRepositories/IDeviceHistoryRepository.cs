using WarrantyRepository.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IDeviceHistoryRepository
    {
        Task<bool> Add(DeviceHistory deviceHistory);
    }
}
