using System.ComponentModel.DataAnnotations;

namespace DotNetMvcIdentity.Models
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
