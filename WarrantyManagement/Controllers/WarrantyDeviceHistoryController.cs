using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyDeviceHistoryController
    {
        private readonly IWarrantyDeviceHistoryRepository _warrantyDeviceHistoryRepository;

        public WarrantyDeviceHistoryController(IWarrantyDeviceHistoryRepository warrantyDeviceHistoryRepository)
        {
            _warrantyDeviceHistoryRepository = warrantyDeviceHistoryRepository;
        }

        [HttpGet("{warrantyId}/{pageNumber}")]
        [HasPermission("VIEW_WARRANTY")]
        public List<WarrantyDeviceHistory> GetWarrantyDeviceHistoryById(int warrantyId, int pageNumber)
        {
            List<WarrantyDeviceHistory> warrantyDeviceHistories = _warrantyDeviceHistoryRepository.GetDeviceByWarrantyId(warrantyId, pageNumber * 3);
            return warrantyDeviceHistories;
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
        public bool Add(WarrantyDeviceHistoryModel warrantyDevcieHistoryModel)
        {
            WarrantyDeviceHistory warrantyDeviceHistory = new WarrantyDeviceHistory
            {
                WarrantyId = warrantyDevcieHistoryModel.WarrantyId,
                DeviceId = warrantyDevcieHistoryModel.DeviceId,
                Description = warrantyDevcieHistoryModel.Description,
                Status = warrantyDevcieHistoryModel.Status,
                Result = warrantyDevcieHistoryModel.Result,
                Modifier = warrantyDevcieHistoryModel.Modifier,
                ModifyDate = DateTime.Now
            };
            if (!_warrantyDeviceHistoryRepository.Add(warrantyDeviceHistory))
                return false;
            return true;
        }
    }
}
