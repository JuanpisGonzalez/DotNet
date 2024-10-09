using ApiMovies.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiMovies.Controllers.ForMovies
{
    public class Movies : ControllerBase
    {
        protected readonly IMovieRepository _movieRepository;
        protected readonly IMapper _mapper;

        public Movies(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }
    }
}
