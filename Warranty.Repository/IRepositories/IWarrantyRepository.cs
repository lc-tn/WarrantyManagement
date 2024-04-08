using WarrantyManagement.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IWarrantyRepository
    {
        bool Save();
        Task<List<Warranty>> GetAllPagination(int pageNumber, int pageSize);
        Task<List<Warranty>> GetAll();
        Task<List<Warranty>> GetByStatus(string status);
        Task<List<Warranty>> GetByUser(string userId);
        Task<List<Warranty>> GetByUserPagination(string userId, int pageNumber, int pageSize);
        Task<List<Warranty>> GetNewWarrantySale(int pageNumber, int pageSize);
        Task<List<Warranty>> GetNewWarrantyTech(int pageNumber, int pageSize);
        Task<int> Total();
        Task<int> TotalByUser(string userId);
        Task<int> TotalNewWarrantySale();
        Task<int> TotalNewWarrantyTech();
        Task<Warranty> GetWarrantyById(int id);
        bool CreateWarranty(Warranty warranty);
        bool EditWarranty(Warranty warranty);
        bool DeleteWarranty(Warranty warranty);
    }
}