using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForCategories
{
    public class Categories : ControllerBase
    {
        protected readonly ICategoryRepository _categoryRepository;
        protected readonly IMapper _mapper;

        public Categories(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
    }
}
