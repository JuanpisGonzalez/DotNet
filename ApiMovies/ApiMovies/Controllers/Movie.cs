using ApiMovies.Models;
using ApiMovies.Models.DTOs;
using ApiMovies.MovieMappers;
using ApiMovies.Repositories;
using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Movie : ControllerBase
    {
        protected readonly ICategoryRepository _categoryRepository;
        protected readonly IMapper _mapper;

        public Movie()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Data.ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ApiMovies;User ID=juanpis;Password=12345;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true");
            _categoryRepository = new CategoryRepository(new Data.ApplicationDbContext(optionsBuilder.Options));
            _mapper = new Mapper(new MapperConfiguration(ctg =>
            {
                ctg.CreateMap<Category, CategoryDTO>().ReverseMap();
                ctg.CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            }));
        }


        /*private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public Movie(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategory()
        {
            var categories = _categoryRepository.GetCategories();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);

            return Ok(categoriesDto);
        }*/
    }

    public class GetCategory : Movie
    {
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
