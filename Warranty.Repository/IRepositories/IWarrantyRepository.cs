using WarrantyManagement.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface IWarrantyRepository
    {
        bool Save();
        Task<List<Warranty>> GetAllPagination(int pageNumber);
        Task<List<Warranty>> GetAll();
        Task<List<Warranty>> GetByStatus(string status);
        Task<List<Warranty>> GetByCustomer(string customerId);
        Task<int> Total();
        Task<Warranty> GetWarrantyById(int id);
        bool CreateWarranty(Warranty warranty);
        bool EditWarranty(Warranty warranty);
        bool DeleteWarranty(Warranty warranty);
    }
}