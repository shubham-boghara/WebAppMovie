using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppMovie.Data;
using WebAppMovie.Dtos;
using WebAppMovie.Models;

namespace WebAppMovie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieAsyncAPIRepo _repo;
        private readonly IMapper _mapper;
        public MoviesController(IMovieAsyncAPIRepo repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDto>>> Index()
        {
            var movies = await _repo.GetAsyncAllMovies();

            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<ActionResult<MovieReadDto>> GetMovieById(int id)
        {
            var movie = await _repo.GetAsyncMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieReadDto>(movie));
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovie(MovieCreateDto movieCreateDto)
        {
            var movie = _mapper.Map<Movie>(movieCreateDto);
            await _repo.CreatAsyncMovie(movie);
            await _repo.SaveAsyncChanges();

            var movieReadDto = _mapper.Map<MovieReadDto>(movie);

            return CreatedAtRoute(nameof(GetMovieById), new { Id = movieReadDto.MovieId }, movieReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
        {
            var movieModelFromRepo = await _repo.GetAsyncMovieById(id);
            if (movieModelFromRepo == null)
            {
                return NotFound();
            }
            var movie_model = _mapper.Map(movieUpdateDto, movieModelFromRepo);

            _repo.UpdateAsyncMovie(movieModelFromRepo);

            await _repo.SaveAsyncChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatialUpdateMovie(int id, JsonPatchDocument<MovieUpdateDto> patchDoc)
        {
            var movieModelFromRepo = await _repo.GetAsyncMovieById(id);
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
            _repo.UpdateAsyncMovie(movieModelFromRepo);
            await _repo.SaveAsyncChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movieModelFromRepo = await _repo.GetAsyncMovieById(id);
            if(movieModelFromRepo == null)
            {
                return NotFound();
            }

             _repo.DeleteAsyncMovie(movieModelFromRepo);

            await _repo.SaveAsyncChanges();

            return NoContent();
        }

    }
}
