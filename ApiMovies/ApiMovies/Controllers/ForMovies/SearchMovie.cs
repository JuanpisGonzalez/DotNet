using ApiMovies.Models.DTOs;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForMovies
{
    [Route("api/[Controller]")]
    [ApiController]
    public class SearchMovie : Movies
    {
        public SearchMovie(IMovieRepository movieRepository, IMapper mapper) : base(movieRepository, mapper) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Handle(string movieName)
        {
            try
            {
                var movie = _movieRepository.SearchMovie(movieName);

                /*if (movies == null)
                {
                    return NotFound();
                }*/
                if (!movie.Any()) {
                    return NotFound();
                }

                var movieDTO = _mapper.Map<MovieDTO>(movie);
                return Ok(movieDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error recovering data");
            }
        }
    }
}
