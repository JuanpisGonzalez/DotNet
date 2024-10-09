using ApiMovies.Data;
using ApiMovies.Models;
using ApiMovies.Repositories.IRepositories;

namespace ApiMovies.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateMovie(Movie movie)
        {
            movie.CreationDate = DateTime.Now;
            _context.Movies.Add(movie);
            return Save();
        }

        public bool DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
            return Save();
        }

        public bool ExistMovie(int id)
        {
            return _context.Movies.Any(x => x.Id == id);
        }

        public bool ExistMovie(string name)
        {
            return _context.Movies.Any(x => x.Name == name); 
        }

        public Movie GetMovie(int id)
        {
            return _context.Movies.FirstOrDefault(x =>x.Id == id);
        }

        public ICollection<Movie> GetMovies()
        {
            return _context.Movies.OrderBy(x => x.Name).ToList();
        }

        public ICollection<Movie> GetMoviesforCategory(int idCategory)
        {
            return _context.Movies.Where(x => x.CategoryId == idCategory).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true: false;
        }

        public IEnumerable<Movie> SearchMovie(string movieName)
        {
            IQueryable<Movie> query = _context.Movies;
            if (!string.IsNullOrEmpty(movieName))
            {
                query = query.Where(m => m.Name.Contains(movieName) || m.Description.Contains(movieName));
            }
            return query.ToList();
        }

        public bool UpdateMovie(Movie movie)
        {
            movie.CreationDate = DateTime.Now;
            _context.Movies.Update(movie);
            return Save();
        }
    }
}
