using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiMovies.Models.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ImageRoute { get; set; }
        public enum ClasificationType { Seven, Thirdteen, Sixteen, Eighteen }
        public ClasificationType Clasification { get; set; }
        public DateTime CreationDate { get; set; }
        public int CategoryId { get; set; }
    }
}
