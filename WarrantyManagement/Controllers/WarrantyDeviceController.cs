using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Migrations;
using WarrantyManagement.Model;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyDeviceController
    {
        private readonly IWarrantyDeviceRepository _warrantyDeviceRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IWarrantyRepository _warrantyRepository;

        public WarrantyDeviceController(IWarrantyDeviceRepository warrantyDeviceRepository, 
                IDeviceRepository deviceRepository, IWarrantyRepository warrantyRepository)
        {
            _warrantyDeviceRepository = warrantyDeviceRepository;
            _deviceRepository = deviceRepository;
            _warrantyRepository = warrantyRepository;
        }

        [HttpPost]
        [HasPermission("CREATE_WARRANTY")]
        public async Task<bool> AddWarrantyDevice(EditWarrantyDeviceModel editWarrantyDeviceModel)
        {
            Warranty warranty = await _warrantyRepository.GetWarrantyById(editWarrantyDeviceModel.WarrantyId);
            Device device = await _deviceRepository.GetDeviceById(editWarrantyDeviceModel.DeviceId);
            WarrantyDevice warrantyDevice = new WarrantyDevice
            {
                WarrantyId = warranty.Id,
                DeviceId = device.Id,
                Status = editWarrantyDeviceModel.Status,
                Result = editWarrantyDeviceModel.Result,
                Description = editWarrantyDeviceModel.Description,
                Modifier = editWarrantyDeviceModel.Modifier,
                ModifyDate = DateTime.Now
            };
            if (!_warrantyDeviceRepository.Edit(warrantyDevice))
                return true;
            return false;
        }

        [HttpPut]
        public async Task<bool> EditWarrantyDevice(EditWarrantyDeviceModel editWarrantyDeviceModel)
        {
            WarrantyDevice warrantyDevice = _warrantyDeviceRepository.GetWarrantyDevice(editWarrantyDeviceModel.WarrantyId,
                                                                            editWarrantyDeviceModel.DeviceId);
            if (editWarrantyDeviceModel.ReplacementDevice != null)
            {
                Device replacementDevice = await _deviceRepository.GetDeviceById(editWarrantyDeviceModel.ReplacementDevice);
                warrantyDevice.ReplacementDevice = replacementDevice.Id;
            }
            if (warrantyDevice != null)
            {
                //warrantyDevice.WarrantyId = editWarrantyDeviceModel.WarrantyId;
                //warrantyDevice.DeviceId = editWarrantyDeviceModel.DeviceId;
                warrantyDevice.Status = editWarrantyDeviceModel.Status;
                warrantyDevice.Result = editWarrantyDeviceModel.Result;
                warrantyDevice.Description = editWarrantyDeviceModel.Description;
                warrantyDevice.Modifier = editWarrantyDeviceModel.Modifier;
                warrantyDevice.ModifyDate = DateTime.Now;
                warrantyDevice.Reason = editWarrantyDeviceModel.Reason;
                
                if (!_warrantyDeviceRepository.Edit(warrantyDevice))
                    return true;
            }
            return false;
        }

        [HttpPut("replacement-device/{warrantyId}/{deviceId}")]
        public async Task<bool> AddReplacementDevice(ReplacementDeviceModel replacementDeviceModel, int warrantyId, int deviceId)
        {
            Device device = await _deviceRepository.GetDeviceById(replacementDeviceModel.ReplacementDevice);
            WarrantyDevice warrantyDevice = _warrantyDeviceRepository.GetWarrantyDevice(warrantyId, deviceId);
            if (warrantyDevice != null)
            {
                warrantyDevice.Modifier = replacementDeviceModel.Modifier;
                warrantyDevice.ModifyDate = DateTime.Now;
                warrantyDevice.ReplacementDevice = device.Id;

                if (!_warrantyDeviceRepository.Edit(warrantyDevice))
                    return true;
            }
            return false;
        }
    }
}
