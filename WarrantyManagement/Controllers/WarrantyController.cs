using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Entities;
using WarrantyManagement.Repositories;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyController : Controller
    {
        private WarrantyRepository _warrantyRepository;

        public WarrantyController(WarrantyRepository warrantyRepository)
        {
            _warrantyRepository = warrantyRepository;
        }

        private bool CheckExistence(int id)
        {
            return _warrantyRepository.CheckExistence(id);
        }

        [HttpGet]
        public IActionResult GetAllWarranty()
        {
            List<Warranty> warranties = new List<Warranty>();
            warranties = _warrantyRepository.GetAll();
            return Ok(warranties);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Warranty warranty)
        {
            if (warranty == null)
                return BadRequest(ModelState);

            if (CheckExistence(warranty.Id))
            {
                ModelState.AddModelError("", "This id alredy exists!");
                return StatusCode(422, ModelState);
            }

            if (!_warrantyRepository.CreateCustomer(warranty))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully creating!");
        }

        [HttpPut]
        public IActionResult EditCustomer([FromBody] Warranty warranty)
        {
            if (warranty == null)
                return BadRequest(ModelState);

            if (!CheckExistence(warranty.Id))
            {
                ModelState.AddModelError("", "This id does not exist!");
                return StatusCode(422, ModelState);
            }

            if (!_warrantyRepository.UpdateCustomer(warranty))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully editing!");
        }
    }
}
