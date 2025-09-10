using System.ComponentModel.DataAnnotations;

namespace PetShop.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ tôi?")]
        public bool RememberMe { get; set; }
    }
}