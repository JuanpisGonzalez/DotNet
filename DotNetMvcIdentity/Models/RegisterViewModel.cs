using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DotNetMvcIdentity.Models
{
    public class RegisterViewModel
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

        [Required(ErrorMessage = "Name is required")]
        public string Name {  get; set; }
        public string Url { get; set; }
        public Int32 CountryCode { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "State is required")]
        public bool State { get; set; }

        //To role selection
        [Display(Name="Select role")]
        public IEnumerable<SelectListItem>? RolesList { get; set; }

        [Display(Name = "Select role")]
        public string? SelectedRole { get; set; }
    }
}
