using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppMovie.Data;
using WebAppMovie.Dtos;
using WebAppMovie.Models;

namespace WebAppMovie.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieAPIRepo _repo;
        private readonly IMapper _mapper;
        public MoviesController(IMovieAPIRepo repo, IMapper mapper) { 
           _repo = repo;
           _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieReadDto>> Index()
        {
            var movies = _repo.GetAllMovies();

            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));
        }

        [HttpGet("{id}")]
        public ActionResult<MovieReadDto> GetMovieById(int id)
        {
            var movie = _repo.GetMovieById(id);

            if(movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieReadDto>(movie));
        }
    }
}
