using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;
using WarrantyRepository.IRepositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyHistoryController
    {
        private readonly IWarrantyHistoryRepository _warrantyHistoryRepository;
        private readonly IWarrantyRepository _warrantyRepository;
        private readonly IUserRepository _userRepository;

        public WarrantyHistoryController(IWarrantyHistoryRepository warrantyHistoryRepository,
            IWarrantyRepository warrantyRepository,
            IUserRepository userRepository)
        {
            _warrantyHistoryRepository = warrantyHistoryRepository;
            _warrantyRepository = warrantyRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{warrantyId}/{pageNumber}/{pageSize}")]
        //[HasPermission("VIEW_WARRANTY")]
        public async Task<List<WarrantyHistoryModel>> GetWarrantyHistoryById(int warrantyId, int pageNumber, int pageSize)
        {
            List<WarrantyHistory> warrantyHistories = 
                await _warrantyHistoryRepository.GetByWarranty(warrantyId, pageNumber * pageSize, pageSize);

            List<WarrantyHistoryModel> warrantyHistoryModels = new();

            foreach (WarrantyHistory warrantyHistory in warrantyHistories)
            {
                WarrantyHistoryModel  warrantyHistoryModel = new WarrantyHistoryModel 
                { 
                    Id = warrantyHistory.Id,
                    Description = warrantyHistory.Description,
                    AppointmentDate = warrantyHistory.AppointmentDate,
                    WarrantyDate = warrantyHistory.WarrantyDate,
                    Status = warrantyHistory.Status,
                    Sale = warrantyHistory.Sale,
                    Technician = warrantyHistory.Technician,
                    Modifier = warrantyHistory.Modifier,
                    ModifyDate = warrantyHistory.ModifyDate
                };
                if (!warrantyHistory.Sale.IsNullOrEmpty())
                {
                    User sale = await _userRepository.GetUserById(warrantyHistory.Sale);
                    warrantyHistoryModel.Sale = sale.Name;
                }
                if (!warrantyHistory.Technician.IsNullOrEmpty())
                {
                    User techinician = await _userRepository.GetUserById(warrantyHistory.Technician);
                    warrantyHistoryModel.Technician = techinician.Name;
                }
                warrantyHistoryModels.Add(warrantyHistoryModel);
            }
            return warrantyHistoryModels;
        }

        [HttpGet("total/{warrantyId}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<int> ToTalWarranty(int warrantyId)
        {
            int total = await _warrantyHistoryRepository.Total(warrantyId);
            return total;
        }

        [HttpPost]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<WarrantyHistory> Add(GetWarrantyHistoryModel warrantyHistoryModel)
        {
            Warranty warranty = await _warrantyRepository.GetWarrantyById(warrantyHistoryModel.Id);
            WarrantyHistory warrantyHistory = new WarrantyHistory
            {
                WarrantyId = warranty.Id,
                Description = warrantyHistoryModel.Description,
                AppointmentDate = warrantyHistoryModel.AppointmentDate,
                Status = warrantyHistoryModel.Status,
                Sale = warrantyHistoryModel.Sale,
                Technician = warrantyHistoryModel.Technician,
                Modifier = warrantyHistoryModel.Modifier,
                ModifyDate = DateTime.Now
            };

            if (warrantyHistoryModel.Status.Equals("Hoàn thành") && warranty.WarrantyDate == null)
            {
                warranty.WarrantyDate = DateTime.Now;
            }
            if (_warrantyHistoryRepository.Add(warrantyHistory))
                return warrantyHistory;
            return null;
        }
    }
}
