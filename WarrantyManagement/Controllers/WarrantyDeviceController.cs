using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyDeviceController
    {
        private readonly IWarrantyDeviceRepository _warrantyDeviceRepository;

        public WarrantyDeviceController(IWarrantyDeviceRepository warrantyDeviceRepository)
        {
            _warrantyDeviceRepository = warrantyDeviceRepository;
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
