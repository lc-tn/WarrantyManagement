using Microsoft.AspNetCore.Mvc;
using WarrantyRepository.IRepositories;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using Microsoft.IdentityModel.Tokens;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyController : Controller
    {
        private readonly IWarrantyRepository _warrantyRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWarrantyDeviceRepository _warrantyDeviceRepository;

        public WarrantyController(IWarrantyRepository warrantyRepository, IDeviceRepository deviceRepository,
            IUserRepository userRepository,
            IWarrantyDeviceRepository warrantyDeviceRepository)
        {
            _warrantyRepository = warrantyRepository;
            _deviceRepository = deviceRepository;
            _userRepository = userRepository;
            _warrantyDeviceRepository = warrantyDeviceRepository;
        }

        [HttpGet("{userId}/{pageNumber}/{pageSize}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<List<WarrantyModel>> GetWarrantyPagination(string userId, int pageNumber, int pageSize)
        {
            User currentUser = await _userRepository.GetUserById(userId);
            List<WarrantyModel> warrantyList = new List<WarrantyModel>();
            List<Warranty> warranties = new();

            if (currentUser.RoleId == 2)
            {
                warranties = await _warrantyRepository.GetAllPagination(pageNumber * pageSize, pageSize);    
            }
            else
            {
                warranties = await _warrantyRepository.GetByUserPagination(userId, pageNumber * pageSize, pageSize);
            }

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

        [HttpGet("new/{userId}/{pageNumber}/{pageSize}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<List<WarrantyModel>> GetNewWarranty(string userId, int pageNumber, int pageSize)
        {
            User currentUser = await _userRepository.GetUserById(userId);
            List<WarrantyModel> warrantyList = new List<WarrantyModel>();
            List<Warranty> warranties = new();

            if (currentUser.RoleId == 3)
            {
                warranties = await _warrantyRepository.GetNewWarrantySale(pageNumber * pageSize, pageSize);
            }
            else if (currentUser.RoleId == 8)
            {
                warranties = await _warrantyRepository.GetNewWarrantyTech(pageNumber * pageSize, pageSize);
            }

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

        [HttpGet]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<List<GetWarrantyByIdModel>> GetAllWarranty()
        {
            List<Warranty> warranties = new List<Warranty>();
            warranties = await _warrantyRepository.GetAll();
            List<GetWarrantyByIdModel> warrantyList = new List<GetWarrantyByIdModel>();
            foreach (Warranty warranty in warranties)
            {
                GetWarrantyByIdModel warrantyModel = new GetWarrantyByIdModel();
                User user = await _userRepository.GetUserById(warranty.CustomerId);

                warrantyModel.Id = warranty.Id;
                warrantyModel.CreateDate = warranty.CreateDate;
                warrantyModel.Description = warranty.Description;
                warrantyModel.AppointmentDate = warranty.AppointmentDate;
                warrantyModel.WarrantyDate = warranty.WarrantyDate;
                warrantyModel.Sale = warranty.Sale;
                warrantyModel.Technician = warranty.Technician;
                warrantyModel.CustomerName = user.UserName;
                warrantyModel.Status = warranty.Status;

                warrantyList.Add(warrantyModel);
            }
            return warrantyList;
        }

        [HttpGet("status/{status}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<List<GetWarrantyByIdModel>> GetWarrantyByStatus(string status)
        {
            List<Warranty> warranties = await _warrantyRepository.GetByStatus(status);
            List<GetWarrantyByIdModel> warrantyList = new List<GetWarrantyByIdModel>();
            foreach (Warranty warranty in warranties)
            {
                GetWarrantyByIdModel warrantyModel = new GetWarrantyByIdModel();
                User user = await _userRepository.GetUserById(warranty.CustomerId);

                warrantyModel.Id = warranty.Id;
                warrantyModel.CreateDate = warranty.CreateDate;
                warrantyModel.Description = warranty.Description;
                warrantyModel.AppointmentDate = warranty.AppointmentDate;
                warrantyModel.WarrantyDate = warranty.WarrantyDate;
                warrantyModel.Sale = warranty.Sale;
                warrantyModel.Technician = warranty.Technician;
                warrantyModel.CustomerName = user.UserName;
                warrantyModel.Status = warranty.Status;

                warrantyList.Add(warrantyModel);
            }
            return warrantyList;
        }

        [HttpGet("user/{userId}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<List<GetWarrantyByIdModel>> GetWarrantyByUser(string userId)
        {
            List<Warranty> warranties = await _warrantyRepository.GetByUser(userId);
            List<GetWarrantyByIdModel> warrantyModels = new List<GetWarrantyByIdModel>();
            foreach (Warranty warranty in warranties)
            {
                GetWarrantyByIdModel warrantyModel = new GetWarrantyByIdModel();
                User user = await _userRepository.GetUserById(warranty.CustomerId);

                warrantyModel.Id = warranty.Id;
                warrantyModel.CreateDate = warranty.CreateDate;
                warrantyModel.Description = warranty.Description;
                warrantyModel.AppointmentDate = warranty.AppointmentDate;
                warrantyModel.WarrantyDate = warranty.WarrantyDate;
                warrantyModel.Sale = warranty.Sale;
                warrantyModel.Technician = warranty.Technician;
                warrantyModel.CustomerName = user.UserName;
                warrantyModel.Status = warranty.Status;

                warrantyModels.Add(warrantyModel);
            }
            return warrantyModels;
        }

        [HttpGet("total-new/{userId}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<int> ToTalNewWarranty(string userId)
        {
            User user = await _userRepository.GetUserById(userId);
            int total = 0;
            if (user.RoleId == 3)
            {
                total = await _warrantyRepository.TotalNewWarrantySale();
            }
            else
            {
                total = await _warrantyRepository.TotalNewWarrantyTech();
            }
                
            return total;
        }

        [HttpGet("total/{userId}")]
        [HasPermission("VIEW_WARRANTY")]
        public async Task<int> ToTalWarrantyByUser(string userId)
        {
            User user = await _userRepository.GetUserById(userId);
            int total = 0;
            if (user.RoleId == 2)
            {
                total = await _warrantyRepository.Total();
            }
            else
            {
                total = await _warrantyRepository.TotalByUser(userId);
            }
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
            List<WarrantyDeviceModel> warrantyDeviceModels = new List<WarrantyDeviceModel>();
            foreach (WarrantyDevice warrantyDevice in warrantyDevices)
            {
                Device device = await _deviceRepository.GetDeviceById(warrantyDevice.DeviceId);
                WarrantyDeviceModel warrantyDeviceModel = new WarrantyDeviceModel();
                warrantyDeviceModel.Id = device.Id;
                warrantyDeviceModel.Name = device.Name;
                warrantyDeviceModel.Status = warrantyDevice.Status;
                warrantyDeviceModel.Result = warrantyDevice.Result;
                warrantyDeviceModel.Description = warrantyDevice.Description;
                warrantyDeviceModel.CategoryId = device.CategoryId;
                warrantyDeviceModel.ReplacementDevice = device.ReplacementDevice;
                warrantyDeviceModel.Reason = warrantyDevice.Reason;
                warrantyDeviceModels.Add(warrantyDeviceModel);
            }
            User user = await _userRepository.GetUserById(warranty.CustomerId);
            if (!warranty.Sale.IsNullOrEmpty())
            {
                User sale = await _userRepository.GetUserById(warranty.Sale);
                getWarrantyModel.Sale = sale.Name;
                getWarrantyModel.SaleId = sale.Id;
            }
            if (!warranty.Technician.IsNullOrEmpty())
            {
                User technician = await _userRepository.GetUserById(warranty.Technician);
                getWarrantyModel.Technician = technician.Name;
                getWarrantyModel.TechnicianId = technician.Id;
            }

            getWarrantyModel.Id = warranty.Id;
            getWarrantyModel.Description = warranty.Description;
            getWarrantyModel.Creator = warranty.Creator;
            getWarrantyModel.CreateDate = warranty.CreateDate;
            getWarrantyModel.AppointmentDate = warranty.AppointmentDate;
            getWarrantyModel.WarrantyDate = warranty.WarrantyDate;

            getWarrantyModel.Status = warranty.Status;
            getWarrantyModel.Device = warrantyDeviceModels;
            getWarrantyModel.CustomerName = user.UserName;
            getWarrantyModel.CustomerId = user.Id;
            return getWarrantyModel;
        }

        [HttpPost]
        [HasPermission("CREATE_WARRANTY")]
        public async Task<IActionResult> CreateWarranty([FromBody] CreateWarrantyModel createWarrantyModel)
        {
            List<DeviceWarrantyModel> deviceWarranties = createWarrantyModel.Device;

            User user = await _userRepository.GetUserById(createWarrantyModel.CustomerId);
            Warranty warranty = new Warranty
            {
                Description = createWarrantyModel.Description,
                Creator = createWarrantyModel.Creator,
                Modifier = null,
                ModifyDate = null,
                CreateDate = DateTime.Now,
                AppointmentDate = null,
                WarrantyDate = null,
                Status = "Chờ xác nhận",
                Sale = null,
                Technician = null,
                CustomerId = user.Id
            };
            if (_warrantyRepository.CreateWarranty(warranty))
            {
                _warrantyRepository.Save();
            }
            if (deviceWarranties.Count == 0)
            {
                _warrantyRepository.DeleteWarranty(warranty);
            }
            foreach (DeviceWarrantyModel deviceWarranty in deviceWarranties)
            {
                Device device = await _deviceRepository.GetDeviceById(deviceWarranty.Id);
                WarrantyDevice warrantyDevice = new()
                {
                    WarrantyId = warranty.Id,
                    DeviceId = device.Id,
                    Status = "Chờ xác nhận",
                    Result = " ",
                    Description = deviceWarranty.Description,
                    Modifier = createWarrantyModel.Creator,
                    ModifyDate = DateTime.Now
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
        [HasPermission("EDIT_WARRANTY")]
        public async Task<IActionResult> EditWarranty([FromBody] EditWarrantyModel editWarrantyModel)
        {
            //DateTime? warrantyDate = null;
            //string formatStr = "yyyy-MM-ddTHH:mm:ss.fffZ";
            if (editWarrantyModel == null)
                return BadRequest(ModelState);

            List<WarrantyDevice> warrantyDevices = new List<WarrantyDevice>();

            Warranty warranty = await _warrantyRepository.GetWarrantyById(editWarrantyModel.Id);
            warranty.Sale = null;
            warranty.Technician = null;

            if (!editWarrantyModel.Sale.IsNullOrEmpty())
            {
                User sale = await _userRepository.GetUserById(editWarrantyModel.Sale);
                warranty.Sale = sale.Id;
            }
            if (!editWarrantyModel.Technician.IsNullOrEmpty())
            {
                User techinician = await _userRepository.GetUserById(editWarrantyModel.Technician);
                warranty.Technician = techinician.Id;
            }
            //if (!editWarrantyModel.WarrantyDate.Equals(""))
            //{
            //    warrantyDate = DateTime.ParseExact(editWarrantyModel.WarrantyDate, formatStr, null);
            //}

            //warranty.Id = editWarrantyModel.Id;

            if (editWarrantyModel.Status.Equals("Hoàn thành") && warranty.WarrantyDate == null)
            {
                warranty.WarrantyDate = DateTime.Now;
            }

            warranty.Description = editWarrantyModel.Description;
            warranty.Modifier = editWarrantyModel.Modifier;
            warranty.ModifyDate = DateTime.Now;
            warranty.AppointmentDate = editWarrantyModel.AppointmentDate;          
            warranty.Status = editWarrantyModel.Status;

            if (_warrantyRepository.EditWarranty(warranty))
            {
                _warrantyRepository.Save();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}