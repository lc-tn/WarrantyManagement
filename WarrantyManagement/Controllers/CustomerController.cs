using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Entities;
using WarrantyManagement.Repositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        private bool CheckExistence(string customerId)
        {
            return _customerRepository.CheckExistence(customerId);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(string id)
        {
            Customer customer = _customerRepository.GetCustomerById(id);
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
        public IActionResult GetAllCustomers() 
        {
            List<Customer> customers = new List<Customer>();
            customers = _customerRepository.GetAll();
            return Ok(customers);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest(ModelState);

            if (CheckExistence(customer.Id))
            {
                ModelState.AddModelError("", "This id alredy exists!");
                return StatusCode(422, ModelState);
            }

            if (!_customerRepository.CreateCustomer(customer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully creating!");
        }

        [HttpPut]
        public IActionResult EditCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest(ModelState);

            if (!CheckExistence(customer.Id))
            {
                ModelState.AddModelError("", "This id does not exist!");
                return StatusCode(422, ModelState);
            }

            if (!_customerRepository.UpdateCustomer(customer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully editing!");
        }
    }
}
