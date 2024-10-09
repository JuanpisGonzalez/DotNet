using ApiMovies.Models.DTOs;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForMovies
{
    public class GetMoviesForCategory : Movies
    {
        public GetMoviesForCategory(IMovieRepository movieRepository, IMapper mapper) : base(movieRepository, mapper) { }

        [HttpGet("{idCategory:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Handle(int idCategory)
        {
            var movies = _movieRepository.GetMoviesforCategory(idCategory);

            if(movies == null)
            {
                return NotFound();
            }

            var moviesDTO = _mapper.Map<List<MovieDTO>>(movies);

            return Ok(moviesDTO);
        }
    }
}
