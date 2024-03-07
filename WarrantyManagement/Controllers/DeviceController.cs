using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceRepository _deviceRepository;

        public DeviceController(DeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        [HttpPost]
        //[HasPermission("CREATE_WARRANTY")]
        public async Task<IActionResult> CreateDeviceAsync([FromBody] DeviceModel deviceModel)
        {
            if (deviceModel == null)
                return BadRequest(ModelState);

            Device device = new Device
            {
                Id = deviceModel.Id,
                Description = deviceModel.Description,
                Name = deviceModel.Name,
                Status = deviceModel.Status,
            };

            if (!await _deviceRepository.Create(device))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200);
        }

        [HttpGet]
        [HasPermission("VIEW_DEVICE")]
        public async Task<List<Device>> GetAllDevice()
        {
            List<Device> devices = new List<Device>();
            devices = await _deviceRepository.GetAll();
            return devices;
        }

        [HttpGet("{id}")]
        [HasPermission("VIEW_DEVICE")]
        public async Task<Device> GetDeviceById(int id)
        {
            Device device = new Device();
            device = await _deviceRepository.GetDeviceById(id);
            return device;
        }

        [HttpGet("device/{userId}")]
        [HasPermission("VIEW_DEVICE")]
        public async Task<List<Device>> GetDeviceByUser(string userId)
        {
            List<Device> devices = new List<Device>();
            devices = await _deviceRepository.GetDeviceByUser(userId);
            return devices;
        }
    }
}
