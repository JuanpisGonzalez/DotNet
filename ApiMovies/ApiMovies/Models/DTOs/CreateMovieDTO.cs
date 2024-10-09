namespace ApiMovies.Models.DTOs
{
    public class CreateMovieDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string ImageRoute { get; set; }
        
        public ClasificationType Clasification { get; set; }
        public DateTime CreationDate { get; set; }
        public int CategoryId { get; set; }
    }
}
