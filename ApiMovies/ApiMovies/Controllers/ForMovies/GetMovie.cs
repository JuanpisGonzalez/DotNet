using ApiMovies.Models.DTOs;
using ApiMovies.Repositories;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForMovies
{
    [Route("api/[Controller]")]
    [ApiController]
    public class GetMovie : Movies
    {
        public GetMovie(IMovieRepository movieRepository, IMapper mapper) : base(movieRepository, mapper) { }

        [HttpGet("{id:int}", Name = "GetMovie")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Handle(int id)
        {

            var movie = _movieRepository.GetMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDTO = _mapper.Map<MovieDTO>(movie);

            return Ok(movieDTO);
        }
    }
}
