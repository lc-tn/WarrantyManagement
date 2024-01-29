using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Repositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController
    {
        private readonly RoleRepository _roleRepository;
        private readonly PermissionRepository _permissionRepository;

        public PermissionController(RoleRepository roleRepository, PermissionRepository permissionRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        [HttpGet("{roleId}")]
        [HasPermission("VIEW_PERMISSION")]
        public async Task<List<Permission>> GetPermissionByRole(int roleId)
        {
            var permissions = new List<Permission>();
            permissions = await _permissionRepository.GetByRoleId(roleId);
            return permissions;
        }

        [HttpGet]
        [HasPermission("VIEW_PERMISSION")]
        public async Task<List<Permission>> GetAllPermission()
        {
            var permissions = new List<Permission>();
            permissions = await _permissionRepository.GetAll();
            return permissions;
        }
    }
}
