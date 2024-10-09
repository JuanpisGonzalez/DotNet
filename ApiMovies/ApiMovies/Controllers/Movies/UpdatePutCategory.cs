using ApiMovies.Models.DTOs;
using ApiMovies.Models;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.Movies
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UpdatePutCategory: Categories
    {
        public UpdatePutCategory(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper) { }

        [HttpPut("{id:int}", Name = "UpdatePutCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Handle(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (categoryDTO == null || id != categoryDTO.Id)
            {
                return BadRequest(ModelState);
            }

            var categoryExist = _categoryRepository.GetCategory(id);

            if (categoryExist == null) { 
                return NotFound($"it couldn´t be find the category with id: {id}");
            }

            var category = _mapper.Map<Category>(categoryDTO);

            if (!_categoryRepository.UpdateCategory(category))
            {
                ModelState.AddModelError("", $"Something was wrong updating the record {category.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
