using System.ComponentModel.DataAnnotations;

namespace DotNetMvcIdentity.Models
{
    public class PasswordRecoverViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "{0} must be between at least {2} characters of lenght", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Password confirm")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
