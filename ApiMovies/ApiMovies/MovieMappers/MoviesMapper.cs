using ApiMovies.Models;
using ApiMovies.Models.DTOs;
using AutoMapper;

namespace ApiMovies.MovieMappers
{
    public class MoviesMapper: Profile
    {
        public MoviesMapper()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
        }
    }
}
