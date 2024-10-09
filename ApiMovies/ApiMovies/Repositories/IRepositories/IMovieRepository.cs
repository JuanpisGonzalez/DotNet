using ApiMovies.Models;

namespace ApiMovies.Repositories.IRepositories
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();
        ICollection<Movie> GetMoviesforCategory(int idCategory);
        IEnumerable<Movie> SearchMovie(string movieName);
        Movie GetMovie(int id);
        bool ExistMovie(int id);
        bool ExistMovie(string name);
        bool CreateMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(Movie movie);
        bool Save();
    }
}
