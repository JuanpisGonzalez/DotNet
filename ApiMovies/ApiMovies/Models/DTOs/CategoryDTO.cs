using System.ComponentModel.DataAnnotations;

namespace ApiMovies.Models.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is obligatory")]
        [MaxLength(100, ErrorMessage = "Max length 100")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
