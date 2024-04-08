using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyRepository.Entities;
using WarrantyRepository.IRepositories;
using WarrantyRepository.Repositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<List<Category>> GetAllDevice()
        {
            List<Category> categories = await _categoryRepository.GetAll();
            return categories;
        }
    }
}
