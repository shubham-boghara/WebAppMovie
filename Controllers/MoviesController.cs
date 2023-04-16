using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}", Name = "GetMovieById")]
        public ActionResult<MovieReadDto> GetMovieById(int id)
        {
            var movie = _repo.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieReadDto>(movie));
        }

        [HttpPost]
        public ActionResult CreateMovie(MovieCreateDto movieCreateDto)
        {
            var movie = _mapper.Map<Movie>(movieCreateDto);
            _repo.CreateMovie(movie);
            _repo.SaveChanges();

            var movieReadDto = _mapper.Map<MovieReadDto>(movie);

            return CreatedAtRoute(nameof(GetMovieById), new { Id = movieReadDto.MovieId }, movieReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
        {
            var movieModelFromRepo = _repo.GetMovieById(id);
            if (movieModelFromRepo == null)
            {
                return NotFound();
            }
            var movie_model = _mapper.Map(movieUpdateDto, movieModelFromRepo);

            _repo.UpdateMovie(movieModelFromRepo);

            _repo.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PatialUpdateMovie(int id, JsonPatchDocument<MovieUpdateDto> patchDoc)
        {
            var movieModelFromRepo = _repo.GetMovieById(id);
            if (movieModelFromRepo == null)
            {
                return NotFound();

            }
            var movieToPatch = _mapper.Map<MovieUpdateDto>(movieModelFromRepo);

            patchDoc.ApplyTo(movieToPatch, ModelState);

            if (!TryValidateModel(movieToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(movieToPatch, movieModelFromRepo);
            _repo.UpdateMovie(movieModelFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMovie(int id)
        {
            var movieModelFromRepo = _repo.GetMovieById(id);
            if(movieModelFromRepo == null)
            {
                return NotFound();
            }

            _repo.DeleteMovie(movieModelFromRepo);

            _repo.SaveChanges();

            return NoContent();
        }

    }
}
