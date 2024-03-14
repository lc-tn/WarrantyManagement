using Microsoft.AspNetCore.Identity;
using WarrantyManagement.Entities;
using WarrantyManagement.Model;

namespace WarrantyRepository.IRepositories
{
    public interface IUserRepository
    {
        Task<string> SignInAsync(SignInModel model);
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        public bool CheckExistence(string id);
        bool Save();
        Task<List<User>> GetAll();
        Task<User> GetUserByname(string name);
        Task<User> GetUserById(string id);
        Task<List<User>> GetUserByRole(int roleId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
    }
}
