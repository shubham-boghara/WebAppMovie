using System.Collections.Generic;
using System.Linq;
using WebAppMovie.Data;
using WebAppMovie.Models;

namespace WebAppMovie.ApiRepository.cs
{
    public class MovieAPIRepo : IMovieAPIRepo
    {
        private readonly MovieDbContext _DB;
        public MovieAPIRepo(MovieDbContext DB) {
          _DB = DB;
        }
        public void CreateMovie(Movie mv)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteMovie(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            var movies = _DB.Movies.ToList();
            return movies;
        }

        public Movie GetMovieById(int id)
        {
            var get_movie = _DB.Movies.FirstOrDefault(c => c.MovieId == id);

            return get_movie;
        }

        public void UpdateMovie(Movie mv)
        {

            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            return _DB.SaveChanges() > 0;
        }
    }
}
