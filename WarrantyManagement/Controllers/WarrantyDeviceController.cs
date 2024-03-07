using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyDeviceController
    {

        private readonly WarrantyRepository _warrantyRepository;
        private readonly DeviceRepository _deviceRepository;
        private readonly UserRepository _userRepository;
        private readonly WarrantyHistoryRepository _warrantyHistoryRepository;
        private readonly WarrantyDeviceRepository _warrantyDeviceRepository;
        private readonly WarrantyDeviceHistoryRepository _warrantyDeviceHistoryRepository;

        public WarrantyDeviceController(WarrantyRepository warrantyRepository, DeviceRepository deviceRepository,
            UserRepository userRepository, WarrantyHistoryRepository warrantyHistoryRepository,
            WarrantyDeviceRepository warrantyDeviceRepository,
            WarrantyDeviceHistoryRepository warrantyDeviceHistoryRepository)
        {
            _warrantyRepository = warrantyRepository;
            _deviceRepository = deviceRepository;
            _userRepository = userRepository;
            _warrantyHistoryRepository = warrantyHistoryRepository;
            _warrantyDeviceRepository = warrantyDeviceRepository;
            _warrantyDeviceHistoryRepository = warrantyDeviceHistoryRepository;
        }

        [HttpPost]
        [HasPermission("CREATE_WARRANTY")]
        public bool AddWarrantyDevice(EditWarrantyDeviceModel editWarrantyDeviceModel)
        {
            WarrantyDevice warrantyDevice = new WarrantyDevice
            {
                WarrantyId = editWarrantyDeviceModel.WarrantyId,
                DeviceId = editWarrantyDeviceModel.DeviceId,
                Status = editWarrantyDeviceModel.Status,
                Result = editWarrantyDeviceModel.Result,
                Description = editWarrantyDeviceModel.Description,
                Modifier = editWarrantyDeviceModel.Modifier,
                ModifyDate = DateTime.UtcNow
            };
            if (!_warrantyDeviceRepository.Edit(warrantyDevice))
                return true;
            return false;
        }

        [HttpPut]
        public bool EditWarrantyDevice(EditWarrantyDeviceModel editWarrantyDeviceModel)
        {
            WarrantyDevice warrantyDevice = new WarrantyDevice
            {
                WarrantyId = editWarrantyDeviceModel.WarrantyId,
                DeviceId = editWarrantyDeviceModel.DeviceId,
                Status = editWarrantyDeviceModel.Status,
                Result = editWarrantyDeviceModel.Result,
                Description = editWarrantyDeviceModel.Description,
                Modifier = editWarrantyDeviceModel.Modifier,
                ModifyDate = DateTime.UtcNow
            };   
            if (!_warrantyDeviceRepository.Edit(warrantyDevice))
                return true;
            return false;
        }
    }
}
