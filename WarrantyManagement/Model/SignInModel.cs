using System.ComponentModel.DataAnnotations;

namespace WarrantyManagement.Model
{
    public class SignInModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
