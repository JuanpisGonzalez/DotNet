using ApiMovies.Models;
using ApiMovies.Models.DTOs;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForCategories
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CreateCategory : Categories
    {
        public CreateCategory(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper) { }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Handle([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createCategoryDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.ExistCategory(createCategoryDTO.Name))
            {
                ModelState.AddModelError("", "Category alredy exists");
                return StatusCode(404, ModelState);
            }

            var category = _mapper.Map<Category>(createCategoryDTO);

            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("", $"Something was wrong saving the record {category.Name}");
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { category.Id }, category);
        }
    }
}
