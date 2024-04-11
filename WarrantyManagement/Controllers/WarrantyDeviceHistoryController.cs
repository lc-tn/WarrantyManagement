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
    public class WarrantyDeviceHistoryController
    {
        private readonly IWarrantyDeviceHistoryRepository _warrantyDeviceHistoryRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IWarrantyRepository _warrantyRepository;

        public WarrantyDeviceHistoryController(IWarrantyDeviceHistoryRepository warrantyDeviceHistoryRepository,
            IDeviceRepository deviceRepository, IWarrantyRepository warrantyRepository)
        {
            _warrantyDeviceHistoryRepository = warrantyDeviceHistoryRepository;
            _deviceRepository = deviceRepository;
            _warrantyRepository = warrantyRepository;
        }

        [HttpGet("{warrantyId}/{pageNumber}/{pageSize}")]
        //[HasPermission("VIEW_WARRANTY")]
        public async Task<List<WarrantyDeviceHistoryModel>> GetWarrantyDeviceHistoryById(int warrantyId, int pageNumber, int pageSize)
        {
            List<WarrantyDeviceHistory> warrantyDeviceHistories = 
                _warrantyDeviceHistoryRepository.GetDeviceByWarrantyId(warrantyId, pageNumber * pageSize, pageSize);
            List<WarrantyDeviceHistoryModel> warrantyDeviceHistoryModels = new();

            foreach (WarrantyDeviceHistory warrantyDeviceHistory in warrantyDeviceHistories)
            {
                Device device = new();
                if (warrantyDeviceHistory.ReplacementDevice.HasValue){
                    device = await _deviceRepository.GetDeviceById(warrantyDeviceHistory.ReplacementDevice);
                }
                WarrantyDeviceHistoryModel warrantyDeviceHistoryModel = new WarrantyDeviceHistoryModel 
                { 
                    Modifier = warrantyDeviceHistory.Modifier,
                    DeviceId = warrantyDeviceHistory.DeviceId,
                    Description = warrantyDeviceHistory.Description,
                    Status = warrantyDeviceHistory.Status,
                    Result = warrantyDeviceHistory.Result,
                    ModifyDate = warrantyDeviceHistory.ModifyDate,
                    ReplacementDevice = warrantyDeviceHistory.ReplacementDevice,
                    ReplacementDeviceName = device.Name
                };
                warrantyDeviceHistoryModels.Add(warrantyDeviceHistoryModel);
            }
            return warrantyDeviceHistoryModels;
        }

        [HttpGet("total/{warrantyId}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<int> ToTalWarranty(int warrantyId)
        {
            int total = await _warrantyDeviceHistoryRepository.Total(warrantyId);
            return total;
        }

        [HttpPost]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<bool> Add(WarrantyDeviceHistoryModel warrantyDevcieHistoryModel)
        {
            Warranty warranty = await _warrantyRepository.GetWarrantyById(warrantyDevcieHistoryModel.WarrantyId);
            Device device = await _deviceRepository.GetDeviceById(warrantyDevcieHistoryModel.DeviceId);
            WarrantyDeviceHistory warrantyDeviceHistory = new WarrantyDeviceHistory
            {
                WarrantyId = warranty.Id,
                DeviceId = device.Id,
                Description = warrantyDevcieHistoryModel.Description,
                Status = warrantyDevcieHistoryModel.Status,
                Result = warrantyDevcieHistoryModel.Result,
                Modifier = warrantyDevcieHistoryModel.Modifier,
                ModifyDate = DateTime.Now,
                Reason = warrantyDevcieHistoryModel.Reason,
            };
            if (warrantyDevcieHistoryModel.ReplacementDevice != null)
            {
                Device replacementDevice = await _deviceRepository.GetDeviceById(warrantyDevcieHistoryModel.ReplacementDevice);
                warrantyDeviceHistory.ReplacementDevice = replacementDevice.Id;
            }
            if (!_warrantyDeviceHistoryRepository.Add(warrantyDeviceHistory))
                return false;
            return true;
        }

        [HttpPut("replacement-device/{warrantyId}/{deviceId}")]
        public async Task<bool> AddReplacementDevice(ReplacementDeviceModel replacementDeviceModel, int warrantyId, int deviceId)
        {
            Device device = await _deviceRepository.GetDeviceById(replacementDeviceModel.ReplacementDevice);
            WarrantyDeviceHistory warrantyDeviceHistory = _warrantyDeviceHistoryRepository.GetWarrantyDeviceHistory
                                                                                (warrantyId, deviceId);
            if (warrantyDeviceHistory != null)
            {
                warrantyDeviceHistory.Id = 0;
                warrantyDeviceHistory.Modifier = replacementDeviceModel.Modifier;
                warrantyDeviceHistory.ModifyDate = DateTime.Now;
                warrantyDeviceHistory.ReplacementDevice = device.Id;
            }
            if (!_warrantyDeviceHistoryRepository.Add(warrantyDeviceHistory))
                return false;
            return true;
        }
    }
}
