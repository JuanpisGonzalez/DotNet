using ApiMovies.Models.DTOs;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForCategories
{
    [Route("api/[Controller]")]
    [ApiController]
    public class GetCategories : Categories
    {
        public GetCategories(ICategoryRepository categoryRepository, IMapper mapper) : base(categoryRepository, mapper) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Handle()
        {
            var categories = _categoryRepository.GetCategories();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);

            return Ok(categoriesDto);
        }
    }
}

