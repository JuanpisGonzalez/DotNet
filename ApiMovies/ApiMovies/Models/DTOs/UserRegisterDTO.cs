using System.ComponentModel.DataAnnotations;

namespace ApiMovies.Models.DTOs
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "user name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
