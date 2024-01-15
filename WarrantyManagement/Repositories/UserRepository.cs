using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WarrantyManagement.Repositories
{
    public class UserRepository
    {
        private readonly WarrantyManagementDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(WarrantyManagementDbContext context, UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration configuration, 
            RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            this._roleManager = roleManager;
        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || !passwordValid)
            {
                return string.Empty;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var userRole = await _userManager.GetRolesAsync(user);
            foreach(var role in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken
                (
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new User
            {
                UserName = signUpModel.UserName,
                Password = signUpModel.Password,
                Name = signUpModel.Name,
                Email = signUpModel.Email,
                Phone = signUpModel.Phone,
                Address = signUpModel.Address
            };
            var result = await _userManager.CreateAsync(user, signUpModel.Password);
            
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(RoleModel.CUSTOMER))
                {
                    await _roleManager.CreateAsync(new IdentityRole(RoleModel.CUSTOMER));
                }

                await _userManager.AddToRoleAsync(user, RoleModel.CUSTOMER);
            }            
            return result;
        }

        public bool CheckExistence(string id)
        {
            return _context.Customers.Any(c => c.UserName.Equals(id));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public List<User> GetAll()
        {
            return _context.Customers.ToList();
        }

        public User GetCustomerById(string id)
        {
            return _context.Customers.Where(c => c.UserName.Equals(id)).SingleOrDefault();
        }

        public bool CreateCustomer(User customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool UpdateCustomer(User customer)
        {
            _context.Update(customer);
            return Save();
        }
    }
}
