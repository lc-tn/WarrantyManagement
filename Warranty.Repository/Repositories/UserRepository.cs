using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;
using WarrantyRepository.IRepositories;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WarrantyManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WarrantyManagementDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPermissionRepository _permissionReporitory;
        private readonly IRoleRepository _rolerRepository;

        public UserRepository(WarrantyManagementDbContext context, UserManager<User> userManager,
            IConfiguration configuration, IRoleRepository rolerRepository,
            RoleManager<IdentityRole> roleManager, IPermissionRepository permissionRepository)
        {
            this._context = context;
            this._userManager = userManager;
            this._configuration = configuration;
            this._roleManager = roleManager;
            this._permissionReporitory = permissionRepository;
            _rolerRepository = rolerRepository;
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
            
            Role role = await _rolerRepository.GetById(user.RoleId);

                authClaims.Add(new Claim(ClaimTypes.Role, role.Name));

            authClaims.Add(new Claim("UserId", user.Id));

            var permissions = await _permissionReporitory.GetByRoleId(user.RoleId);
            foreach (var permission in permissions)
            {
                authClaims.Add(new Claim("Permission", permission.Name.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken
                (
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(60),
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
                Address = signUpModel.Address,
                RoleId = 1
            };
            var result = await _userManager.CreateAsync(user, signUpModel.Password);
            
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Customer"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Customer"));
                }

                await _userManager.AddToRoleAsync(user, "Customer");
            }            
            return result;
        }

        public bool CheckExistence(string id)
        {
            return _context.Customers.Any(c => c.UserName.Equals(id) || c.Id.Equals(id));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<User> GetUserByname (string name)
        {
            return await _context.Customers.SingleAsync(c => c.UserName.Equals(name));
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Customers.SingleAsync(c => c.Id.Equals(id));
        }

        public async Task<List<User>> GetUserByRole(int roleId)
        {
            return await _context.Customers.Where(c => c.RoleId.Equals(roleId)).ToListAsync();
        }

        public bool CreateUser(User user)
        {
            var entry = _context.Add(user);
            if (entry.State == EntityState.Added)
            {
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}
