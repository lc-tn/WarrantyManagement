using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUserRepository _userRepository;

        public DeviceController(IDeviceRepository deviceRepository, IUserRepository userRepository)
        {
            _deviceRepository = deviceRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        //[HasPermission("CREATE_WARRANTY")]
        public async Task<IActionResult> CreateDevice([FromBody] DeviceModel deviceModel)
        {
            if (deviceModel == null)
                return BadRequest(ModelState);

            Device device = new()
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
        public async Task<List<DeviceModel>> GetAllDevice()
        {
            List<DeviceModel> deviceModels = new();
            List<Device> devices = await _deviceRepository.GetAll();
            foreach (Device device in devices)
            {
                User user = new();
                if (!device.UserId.IsNullOrEmpty())
                {
                    user = await _userRepository.GetUserById(device.UserId);
                }
                DeviceModel deviceModel = new DeviceModel
                {
                    Id = device.Id,
                    Name = device.Name,
                    UserName = user.Name,
                    ReplacementDevice = device.ReplacementDevice,
                    CategoryId = device.CategoryId,
                    ActivationDate = device.ActivationDate,
                    WarrantyPeriod = device.WarrantyPeriod
                };
                deviceModels.Add(deviceModel);
            }
            return deviceModels;
        }

        [HttpGet("{id}")]
        [HasPermission("VIEW_DEVICE")]
        public async Task<Device> GetDeviceById(int id)
        {
            Device device = await _deviceRepository.GetDeviceById(id);
            return device;
        }

        [HttpGet("device/{userId}")]
        [HasPermission("VIEW_DEVICE")]
        public async Task<List<Device>> GetDeviceByUser(string userId)
        {
            List<Device> devices = await _deviceRepository.GetDeviceByUser(userId);
            return devices;
        }

        [HttpGet("replacement-device/{categoryId}")]
        [HasPermission("VIEW_DEVICE")]
        public async Task<List<Device>> GetReplacementDevice(int categoryId)
        {
            List<Device> devices = await _deviceRepository.GetReplacementDevice(categoryId);
            return devices;
        }

        [HttpPut]
        //[HasPermission("EDIT_WARRANTY")]
        public async Task<IActionResult> AddReplacementDevice([FromBody] ReplacementDeviceModel replacementDeviceModel)
        {
            List<Device> devices = new();
            Device device = await _deviceRepository.GetDeviceById(replacementDeviceModel.Id);
            device.Status = "Thay thế";
            device.ReplacementDevice = replacementDeviceModel.ReplacementDevice;
            device.Modifier = replacementDeviceModel.Modifier;
            device.ModifyDate = DateTime.Now;
            devices.Add(device);

            Device replacementDevice = await _deviceRepository.GetDeviceById(replacementDeviceModel.ReplacementDevice);
            replacementDevice.Status = "Đang sử dụng";
            replacementDevice.UserId = replacementDeviceModel.UserId;
            replacementDevice.Modifier = replacementDeviceModel.Modifier;
            replacementDevice.ModifyDate = DateTime.Now;
            devices.Add(replacementDevice);

            if (_deviceRepository.EditDevices(devices))
            {
                _deviceRepository.Save();
                return StatusCode(200);
            } 
            return StatusCode(500);
        }

        [HttpPut("edit/{deviceId}/{status}")]
        public async Task<IActionResult> EditDevice(string status, int deviceId)
        {
            Device device = await _deviceRepository.GetDeviceById(deviceId);
            device.Status = status;

            List<Device> devices = new List<Device> { device };
            if (_deviceRepository.EditDevices(devices))
            {
                _deviceRepository.Save();
                return StatusCode(200);
            }
            return StatusCode(500);
        }
    }
}
