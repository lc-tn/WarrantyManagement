using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
        private readonly WarrantyHistoryRepository _warrantyHistoryRepository;
        private readonly WarrantyDeviceRepository _warrantyDeviceRepository;
        private readonly WarrantyDeviceHistoryRepository _warrantyDeviceHistoryRepository;

        public WarrantyController(WarrantyRepository warrantyRepository, DeviceRepository deviceRepository,
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

        private bool CheckExistence(int id)
        {
            return _warrantyRepository.CheckExistence(id);
        }

        [HttpGet("paging/{pageNumber}")]
        //[HasPermission("VIEW_WARRANTY")]
        public async Task<List<WarrantyModel>> GetAllWarranty(int pageNumber)
        {
            List<Warranty> warranties = new List<Warranty>();
            warranties = await _warrantyRepository.GetAll(pageNumber * 3);
            List<WarrantyModel> warrantyList = new List<WarrantyModel>();
            foreach (Warranty warranty in warranties)
            {
                WarrantyModel warrantyModel = new WarrantyModel();
                User user = await _userRepository.GetUserById(warranty.CustomerId);

                warrantyModel.Id = warranty.Id;
                warrantyModel.CreateDate = warranty.CreateDate;
                warrantyModel.Status = warranty.Status;
                warrantyModel.CustomerName = user.UserName;

                warrantyList.Add(warrantyModel);
            }
            return warrantyList;
        }

        [HttpGet("total")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<int> ToTalWarranty()
        {
            int total = await _warrantyRepository.Total();
            return total;
        }

        [HttpGet("{id}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<GetWarrantyByIdModel> GetWarrantyById(int id)
        {
            Warranty warranty = new Warranty();
            warranty = await _warrantyRepository.GetWarrantyById(id);

            GetWarrantyByIdModel getWarrantyModel = new GetWarrantyByIdModel();
            List<WarrantyDevice> warrantyDevices = _warrantyDeviceRepository.GetDeviceByWarrantyId(warranty.Id);
            List<WarrantyDeviceModel> devices = new List<WarrantyDeviceModel>();
            foreach (WarrantyDevice warrantyDevice in warrantyDevices)
            {
                Device device = await _deviceRepository.GetDeviceById(warrantyDevice.DeviceId);
                WarrantyDeviceModel editDeviceModel = new WarrantyDeviceModel();
                editDeviceModel.Id = device.Id;
                editDeviceModel.Name = device.Name;
                editDeviceModel.Status = warrantyDevice.Status;
                editDeviceModel.Result = warrantyDevice.Result;
                editDeviceModel.Description = warrantyDevice.Description;
                devices.Add(editDeviceModel);
            }
            User user = await _userRepository.GetUserById(warranty.CustomerId);

            getWarrantyModel.Id = warranty.Id;
            getWarrantyModel.Description = warranty.Description;
            getWarrantyModel.Creator = warranty.Creator;
            getWarrantyModel.CreateDate = warranty.CreateDate;
            getWarrantyModel.AppointmentDate = warranty.AppointmentDate;
            getWarrantyModel.WarrantyDate = warranty.WarrantyDate;
            getWarrantyModel.Sale = warranty.Sale;
            getWarrantyModel.Technician = warranty.Technician;
            getWarrantyModel.Status = warranty.Status;
            getWarrantyModel.Device = devices;
            getWarrantyModel.CustomerName = user.UserName;
            return getWarrantyModel;
        }

        [HttpPost]
        [HasPermission("CREATE_WARRANTY")]
        public async Task<IActionResult> CreateWarranty([FromBody] CreateWarrantyModel createWarrantyModel)
        {
            List<DeviceWarrantyModel> deviceWarranty = createWarrantyModel.Device;

            User user = await _userRepository.GetUserById(createWarrantyModel.CustomerId);
            Warranty warranty = new Warranty
            {
                Description = createWarrantyModel.Description,
                Creator = createWarrantyModel.Creator,
                Modifier = " ",
                ModifyDate = null,
                CreateDate = DateTime.UtcNow,
                AppointmentDate = createWarrantyModel.AppointmentDate,
                WarrantyDate = null,
                Status = "Chờ xác nhận",
                Sale = "",
                Technician = "",
                CustomerId = user.Id
            };
            if (_warrantyRepository.CreateWarranty(warranty))
            {
                _warrantyRepository.Save();
            }
            if (deviceWarranty.Count == 0)
            {
                _warrantyRepository.DeleteWarranty(warranty);
            }
            foreach (DeviceWarrantyModel device in deviceWarranty)
            {
                WarrantyDevice warrantyDevice = new WarrantyDevice
                {
                    WarrantyId = warranty.Id,
                    DeviceId = device.Id,
                    Status = "Chờ xác nhận",
                    Result = " ",
                    Description = device.Description,
                    Modifier = createWarrantyModel.Creator,
                    ModifyDate = DateTime.UtcNow
                };
                if (!_warrantyDeviceRepository.Add(warrantyDevice))
                {
                    _warrantyRepository.DeleteWarranty(warranty);
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }
            }
            return Ok(warranty);
        }

        [HttpPut]
        //[HasPermission("EDIT_WARRANTY")]
        public async Task<IActionResult> EditWarranty ([FromBody] EditWarrantyModel editWarrantyModel)
        {
            //DateTime? warrantyDate = null;
            //string formatStr = "yyyy-MM-ddTHH:mm:ss.fffZ";
            if (editWarrantyModel == null)
                return BadRequest(ModelState);

            if (!CheckExistence(editWarrantyModel.Id))
            {
                ModelState.AddModelError("", "This id does not exist!");
                return StatusCode(422, ModelState);
            }    
            List<WarrantyDevice> warrantyDevices = new List<WarrantyDevice>();

            Warranty warranty = await _warrantyRepository.GetWarrantyById(editWarrantyModel.Id);
            warranty.Sale = "";
            warranty.Technician = "";

            if (!editWarrantyModel.Sale.Equals(""))
            {
                User sale = await _userRepository.GetUserByname(editWarrantyModel.Sale);
                warranty.Sale = sale.UserName;
            }
            if (!editWarrantyModel.Technician.Equals(""))
            {
                User techinician = await _userRepository.GetUserByname(editWarrantyModel.Technician);
                warranty.Technician = techinician.UserName;
            }
            //if (!editWarrantyModel.WarrantyDate.Equals(""))
            //{
            //    warrantyDate = DateTime.ParseExact(editWarrantyModel.WarrantyDate, formatStr, null);
            //}

            warranty.Id = editWarrantyModel.Id;
            warranty.Description = editWarrantyModel.Description;
            warranty.Modifier = editWarrantyModel.Modifier;
            warranty.ModifyDate = DateTime.Now;
            warranty.AppointmentDate = editWarrantyModel.AppointmentDate;
            warranty.WarrantyDate = editWarrantyModel.WarrantyDate;
            warranty.Status = editWarrantyModel.Status;            

            if (_warrantyRepository.EditWarranty(warranty))
            {
                _warrantyRepository.Save();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }            return Ok();
        }
    }
}