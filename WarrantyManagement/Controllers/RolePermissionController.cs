using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly RolePermissionRepository _rolePermissionRepository;
        private readonly RoleRepository _roleRepository;
        private readonly PermissionRepository _permissionRepository;

        public RolePermissionController(RolePermissionRepository rolePermissionRepository,
            RoleRepository roleRepository, PermissionRepository permissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
        }

        [HttpPost]
        [HasPermission("EDIT_PERMISSION")]
        public async Task<IActionResult> EditRolePermission(RolePermissionRequestModel rolePermissionModel)
        {
            bool check = await _rolePermissionRepository.DeleteByRole(rolePermissionModel.RoleId);
            if (check)
            {
                List<int> permissionIds = rolePermissionModel.PermissionId;
                foreach (int permissionId in permissionIds)
                {
                    RolePermission rolePermission = new RolePermission()
                    {
                        RoleId = rolePermissionModel.RoleId,
                        Role = await _roleRepository.GetById(rolePermissionModel.RoleId),
                        PermissionId = permissionId,
                        Permission = await _permissionRepository.GetById(permissionId),
                    };
                    await _rolePermissionRepository.Add(rolePermission);
                }
            }
            return Ok("Successfully creating!");
        }
    }
}
