using WarrantyManagement.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IWarrantyHistoryRepository
    {
        bool Save();
        bool Add(WarrantyHistory warrantyHistory);
        Task<List<WarrantyHistory>> GetByWarranty(int warrantyId, int pageNumber, int pageSize);
        Task<int> Total(int warrantyId);
    }
}
