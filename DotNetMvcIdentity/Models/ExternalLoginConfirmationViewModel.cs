﻿using System.ComponentModel.DataAnnotations;

namespace DotNetMvcIdentity.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
