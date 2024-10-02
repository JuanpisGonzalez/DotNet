using Microsoft.AspNetCore.Identity;

namespace DotNetMvcIdentity.Models
{
    public class AppUser: IdentityUser
    {
        public string? Id {  get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Int32 CountryCode { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public bool State {  get; set; }
    }
}
