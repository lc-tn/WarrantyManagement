using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyController : Controller
    {
        private readonly WarrantyRepository _warrantyRepository;
        private readonly DeviceRepository _deviceRepository;
        private readonly UserRepository _userRepository;

        public WarrantyController(WarrantyRepository warrantyRepository, DeviceRepository deviceRepository,
            UserRepository userRepository)
        {
            _warrantyRepository = warrantyRepository;
            _deviceRepository = deviceRepository;
            _userRepository = userRepository;
        }

        private bool CheckExistence(int id)
        {
            return _warrantyRepository.CheckExistence(id);
        }

        [HttpGet]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<List<WarrantyModel>> GetAllWarranty()
        {
            List<Warranty> warranties = new List<Warranty>();
            warranties = await _warrantyRepository.GetAll();
            List<WarrantyModel> warrantyList = new List<WarrantyModel>();
            foreach (Warranty warranty in warranties)
            {
                WarrantyModel warrantyModel = new WarrantyModel();
                Device device = await _deviceRepository.GetDeviceByIdAsnc(warranty.DeviceId);
                User user = await _userRepository.GetCustomerById(warranty.CustomerId);

                warrantyModel.Id = warranty.Id;
                warrantyModel.Description = warranty.Description;
                warrantyModel.CreateDate = warranty.CreateDate;
                //warrantyModel.ApointmentDate = warrantyModel.ApointmentDate;
                warrantyModel.Status = warranty.Status;
                warrantyModel.DeviceName = device.Name;
                warrantyModel.CustomerName = user.UserName;

                warrantyList.Add(warrantyModel);
            }
            
            return warrantyList;
        }

        [HttpPost]
        [HasPermission("CREATE_WARRANTY")]
        public async Task<IActionResult> CreateWarrantyAsync([FromBody] WarrantyModel warrantyModel)
        {
            Device device = await _deviceRepository.GetDeviceByNameAsnc(warrantyModel.DeviceName);
            User user = await _userRepository.GetCustomerByname(warrantyModel.CustomerName);
            Warranty warranty = new Warranty
            {
                Id = warrantyModel.Id,
                Description = warrantyModel.Description,
                CreateDate = warrantyModel.CreateDate,
                //ApointmentDate = warrantyModel.ApointmentDate,
                Status = warrantyModel.Status,
                DeviceId = device.Id,
                CustomerId = user.Id
            };
            if (warranty == null)
                return BadRequest(ModelState);

            if (CheckExistence(warranty.Id))
            {
                ModelState.AddModelError("", "This id alredy exists!");
                return StatusCode(422, ModelState);
            }

            if (!_warrantyRepository.CreateWarranty(warranty))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully creating!");
        }

        [HttpPut]
        [HasPermission("EDIT_WARRANTY")]
        public async Task<IActionResult> EditWarrantyAsync([FromBody] WarrantyModel warrantyModel)
        {
            if (warrantyModel == null)
                return BadRequest(ModelState);

            if (!CheckExistence(warrantyModel.Id))
            {
                ModelState.AddModelError("", "This id does not exist!");
                return StatusCode(422, ModelState);
            }

            Device device = await _deviceRepository.GetDeviceByNameAsnc(warrantyModel.DeviceName);
            User user = await _userRepository.GetCustomerByname(warrantyModel.CustomerName);

            Warranty warranty = await _warrantyRepository.GetWarrantyById(warrantyModel.Id);
            warranty.Id = warrantyModel.Id;
            warranty.Description = warrantyModel.Description;
            warranty.CreateDate = warrantyModel.CreateDate;
            warranty.Status = warrantyModel.Status;
            warranty.DeviceId = device.Id;
            warranty.CustomerId = user.Id;

            if (!_warrantyRepository.UpdateCustomer(warranty))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully editing!");
        }
    }
}
