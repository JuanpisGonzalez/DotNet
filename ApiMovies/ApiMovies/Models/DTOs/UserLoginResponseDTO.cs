namespace ApiMovies.Models.DTOs
{
    public class UserLoginResponseDTO
    {
        public userDataDTO User {  get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
