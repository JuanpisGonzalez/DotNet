using ApiMovies.Repositories;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForMovies
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DeleteMovie : Movies
    {
        public DeleteMovie(IMovieRepository movieRepository, IMapper mapper) : base(movieRepository, mapper) { }


        [HttpDelete("{id:int}", Name = "DeleteMovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Handle(int id)
        {

            if (!_movieRepository.ExistMovie(id))
            {
                return NotFound($"it couldn´t be find the movie with id: {id}");
            }
            var movie = _movieRepository.GetMovie(id);

            if (!_movieRepository.DeleteMovie(movie))
            {
                ModelState.AddModelError("", $"Something was wrong removing the movie with id : {id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
