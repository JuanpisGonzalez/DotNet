using ApiMovies.Controllers.Movies;
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
    public class Categories : ControllerBase
    {
        protected readonly ICategoryRepository _categoryRepository;
        protected readonly IMapper _mapper;

        public Categories(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
           /* _categoryRepository = new CategoryRepository(new Data.ApplicationDbContext(OptionsBuilder.GetOptions().Options));
            _mapper = new Mapper(new MapperConfiguration(ctg =>
            {
                ctg.CreateMap<Category, CategoryDTO>().ReverseMap();
                ctg.CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            }));*/
        }
    }
}
