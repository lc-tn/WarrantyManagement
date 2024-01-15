using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private bool CheckExistence(string customerId)
        {
            return _userRepository.CheckExistence(customerId);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await _userRepository.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return StatusCode(500);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await _userRepository.SignInAsync(signInModel);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(string id)
        {
            User customer = _userRepository.GetCustomerById(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public List<User> GetAllCustomers() 
        {
            List<User> customers = new List<User>();
            customers = _userRepository.GetAll();
            return customers;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateCustomer([FromBody] User customer)
        {
            if (customer == null)
                return BadRequest(ModelState);

            if (CheckExistence(customer.UserName))
            {
                ModelState.AddModelError("", "This id alredy exists!");
                return StatusCode(422, ModelState);
            }

            if (!_userRepository.CreateCustomer(customer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully creating!");
        }

        [HttpPut]
        [Authorize]
        public IActionResult EditCustomer([FromBody] User customer)
        {
            if (customer == null)
                return BadRequest(ModelState);

            if (!CheckExistence(customer.UserName))
            {
                ModelState.AddModelError("", "This id does not exist!");
                return StatusCode(422, ModelState);
            }

            if (!_userRepository.UpdateCustomer(customer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully editing!");
        }
    }
}
