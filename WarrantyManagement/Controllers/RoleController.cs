using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;
using Role = WarrantyManagement.Entities.Role;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;
        private readonly PermissionRepository _permissionRepository;

        public RoleController(RoleRepository roleRepository, PermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
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
