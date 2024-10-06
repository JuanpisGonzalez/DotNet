using System.ComponentModel.DataAnnotations;

namespace ApiMovies.Models.DTOs
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Name is obligatory")]
        [MaxLength(100, ErrorMessage = "Max length 100!")]
        public string Name { get; set; }

    }
}
