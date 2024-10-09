using ApiMovies.Models.DTOs;
using ApiMovies.Models;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.Movies
{
    [Route("api/[Controller]")]
    [ApiController]
    public class DeleteCategory : Categories
    {
        public DeleteCategory(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper) { }

        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Handle(int id)
        {

            if (!_categoryRepository.ExistCategory(id))
            {
                return NotFound($"it couldn´t be find the category with id: {id}");
            }
            var category = _categoryRepository.GetCategory(id);

            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("", $"Something was wrong removing the category with id : {id}");
                return StatusCode(500, ModelState);
            }


            return NoContent();
        }
    }
}

