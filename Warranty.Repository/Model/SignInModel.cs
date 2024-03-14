using System.ComponentModel.DataAnnotations;

namespace WarrantyManagement.Model
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
