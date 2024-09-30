using System.ComponentModel.DataAnnotations;

namespace DotNetMvcIdentity.Models
{
    public class TwoFactoAuthenticationViewModel
    {
        [Required]
        [Display(Name = "Authenticator code")]
        public string Code { get; set; }

        //To registry
        public string Token { get; set; }

        //To qr code
        public string urlQrCode { get; set; }
    }
}
