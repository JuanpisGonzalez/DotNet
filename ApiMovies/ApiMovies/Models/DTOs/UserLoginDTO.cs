using System.ComponentModel.DataAnnotations;

namespace ApiMovies.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "user name is required")]
        public string UserName { get; set; }
   
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
    }
}
