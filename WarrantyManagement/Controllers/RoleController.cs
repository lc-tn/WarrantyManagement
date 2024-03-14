using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;
using WarrantyRepository.IRepositories;
using Role = WarrantyManagement.Entities.Role;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [HasPermission("VIEW_ROLE")]
        public async Task<List<Role>> GetAll()
        {
            var roles = new List<Role>();
            roles = await _roleRepository.GetAll();
            return roles;
        }

        [HttpPost]
        [HasPermission("CREATE_ROLE")]
        public async Task<IActionResult> CreateRole(RoleModel roleModel)
        {
            var role = new Role()
            {
                Id = roleModel.Id,
                Name = roleModel.Name
            };

            var creatingRole = await _roleRepository.CreateRole(role);
            if (creatingRole != null)
            {
                return Ok(creatingRole);
            }
            return NotFound();
        }
    }
}
