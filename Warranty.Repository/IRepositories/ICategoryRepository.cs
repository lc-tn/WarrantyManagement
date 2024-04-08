using WarrantyRepository.Entities;

namespace WarrantyRepository.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
    }
}
