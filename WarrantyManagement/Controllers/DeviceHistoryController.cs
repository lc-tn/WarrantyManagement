using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Model;
using WarrantyRepository.Entities;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceHistoryController : ControllerBase
    {
        private readonly IDeviceHistoryRepository _deviceHistoryRepository;

        public DeviceHistoryController(IDeviceHistoryRepository deviceHistoryRepository)
        {
            _deviceHistoryRepository = deviceHistoryRepository;
        }

        [HttpPost]
        //[HasPermission("CREATE_WARRANTY")]
        public async Task<IActionResult> Add([FromBody] DeviceModel deviceModel)
        {
            if (deviceModel == null)
                return BadRequest(ModelState);

            DeviceHistory deviceHistory = new()
            {
                Id = deviceModel.Id,
                Description = deviceModel.Description,
                Name = deviceModel.Name,
                Status = deviceModel.Status,
            };

            if (!await _deviceHistoryRepository.Add(deviceHistory))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return StatusCode(200);
        }
    }
}
