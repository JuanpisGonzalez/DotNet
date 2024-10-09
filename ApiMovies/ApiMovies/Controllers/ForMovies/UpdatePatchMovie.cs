using ApiMovies.Models.DTOs;
using ApiMovies.Models;
using ApiMovies.Repositories;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForMovies
{
    public class UpdatePatchMovie : Movies
    {
        public UpdatePatchMovie(IMovieRepository movieRepository, IMapper mapper) : base(movieRepository, mapper) { }

        [HttpPatch("{id:int}", Name = "UpdatePatchMovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Handle(int id, [FromBody] MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (movieDTO == null || id != movieDTO.Id)
            {
                return BadRequest(ModelState);
            }

            if (_movieRepository.ExistMovie(id) == false)
            {
                return NotFound($"it couldn´t be find the movie with id: {id}");
            }

            var movie = _mapper.Map<Movie>(movieDTO);

            if (!_movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("", $"Something was wrong updating the record {movie.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
