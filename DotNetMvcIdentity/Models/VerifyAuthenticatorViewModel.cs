using System.ComponentModel.DataAnnotations;

namespace DotNetMvcIdentity.Models
{
    public class VerifyAuthenticatorViewModel
    {
        [Required]
        [Display(Name = "Authenticator code")]
        public string Code { get; set; }

        public string? ReturnUrl { get; set; }

        [Display(Name = "Remember data?")]
        public bool RememberData { get; set; }
    }
}
