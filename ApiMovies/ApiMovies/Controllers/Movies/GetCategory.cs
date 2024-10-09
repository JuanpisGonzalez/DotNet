using ApiMovies.Models.DTOs;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.Movies
{
    [Route("api/[Controller]")]
    [ApiController]
    public class GetCategory: Categories
    {
        public GetCategory(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper) { }

        [HttpGet("{id:int}", Name="GetCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Handle(int id)
        {
            var category = _categoryRepository.GetCategory(id);

            if (category == null) {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return Ok(categoryDto);
        }
    }
}
