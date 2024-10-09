using Microsoft.EntityFrameworkCore;

namespace ApiMovies.Controllers.Movies
{
    public static class OptionsBuilder
    {
        public static DbContextOptionsBuilder<Data.ApplicationDbContext> GetOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Data.ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ApiMovies;User ID=juanpis;Password=12345;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true");
            return optionsBuilder;
        }   
    }
}
