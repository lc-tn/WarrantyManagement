using Microsoft.AspNetCore.Mvc;
using WarrantyManagement.Authorization;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyManagement.Repositories;
using WarrantyRepository.IRepositories;
using Role = WarrantyManagement.Entities.Role;

namespace WarrantyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserController(IUserRepository userRepository,
             IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        private bool CheckExistence(string userId)
        {
            return _userRepository.CheckExistence(userId);
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
        public async Task<string> SignIn(SignInModel signInModel)
        {
            string result = await _userRepository.SignInAsync(signInModel);
            if (string.IsNullOrEmpty(result))
            {
                return null;
            }
            return result;
        }

        [HttpGet("{id}")]
        [HasPermission("VIEW_USER")]
        public async Task<IActionResult> GetUserById(string id)
        {
            User user = await _userRepository.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetUserByRole(int roleId)
        {
            List<User> users =  await _userRepository.GetUserByRole(roleId);
            if (users != null)
            {
                return Ok(users);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [HasPermission("VIEW_USER")]
        public async Task<List<UserModel>> GetAll() 
        {
            List<User> users = new List<User>();
            List<UserModel> userModels = new List<UserModel>();
            users = await _userRepository.GetAll();
            foreach(User user in users)
            {
                UserModel userModel = new UserModel();
                Role role = await _roleRepository.GetById(user.RoleId);
                userModel.Id = user.Id;
                userModel.Password = user.Password;
                userModel.Name = user.Name;
                userModel.Email = user.Email;
                userModel.Phone = user.Phone;
                userModel.Address = user.Address;
                userModel.Role = role.Name;
                userModels.Add(userModel);
            }
            return userModels;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest(ModelState);

            if (CheckExistence(user.UserName))
            {
                ModelState.AddModelError("", "This id alredy exists!");
                return StatusCode(422, ModelState);
            }

            if (!_userRepository.CreateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully creating!");
        }

        [HttpPut]
        [HasPermission("EDIT_USER")]
        public async Task<IActionResult> EditUser([FromBody] UserModel userModel)
        {
            if (userModel == null)
                return BadRequest(ModelState);

            if (!CheckExistence(userModel.Id))
            {
                ModelState.AddModelError("", "This id does not exist!");
                return StatusCode(422, ModelState);
            }

            Role role = await _roleRepository.GetByName(userModel.Role);

            User user = await _userRepository.GetUserById(userModel.Id);
            user.Name = userModel.Name;
            user.Password = userModel.Password;
            user.Email = userModel.Email;
            user.Phone = userModel.Phone;
            user.Address = userModel.Address;
            user.RoleId = role.Id;
            user.Role = role;

            if (!_userRepository.UpdateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully editing!");
        }
    }
}
