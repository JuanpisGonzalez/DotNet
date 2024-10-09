using ApiMovies.Models;
using ApiMovies.Models.DTOs;
using ApiMovies.Repositories;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForMovies
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CreateMovie : Movies
    {
        public CreateMovie(IMovieRepository movieRepository, IMapper mapper) : base(movieRepository, mapper) { }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateMovieDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Handle([FromBody] CreateMovieDTO createMovieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createMovieDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_movieRepository.ExistMovie(createMovieDTO.Name))
            {
                ModelState.AddModelError("", "Movie alredy exists");
                return StatusCode(404, ModelState);
            }

            var movie = _mapper.Map<Movie>(createMovieDTO);

            if (!_movieRepository.CreateMovie(movie))
            {
                ModelState.AddModelError("", $"Something was wrong saving the record {movie.Name}");
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetMovie", new { movie.Id }, movie);
        }
    }
}
