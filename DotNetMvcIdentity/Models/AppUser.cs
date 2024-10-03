using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace DotNetMvcIdentity.Models
{
    public class AppUser: IdentityUser
    {
       // public string? Id {  get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Int32 CountryCode { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime Birthdate { get; set; }
        public bool State {  get; set; }
        //role assignation
        [NotMapped]
        [Display(Name = "user role")]
        public string IdRol {  get; set; }
        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? Roles { get; set; }
    }
}
