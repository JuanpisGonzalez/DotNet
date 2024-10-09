using ApiMovies.Models.DTOs;
using ApiMovies.Repositories;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForMovies
{
    [Route("api/[Controller]")]
    [ApiController]
    public class GetMovies : Movies
    {
        public GetMovies(IMovieRepository movieRepository, IMapper mapper) : base(movieRepository, mapper) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Handle()
        {
            var movies = _movieRepository.GetMovies();
            var moviesDTO = _mapper.Map<List<MovieDTO>>(movies);

            return Ok(moviesDTO);
        }
    }
}
