using System.Collections;
using System.Collections.Generic;
using WebAppMovie.Models;

namespace WebAppMovie.Data
{
    public interface IMovieAPIRepo
    {
        public IEnumerable<Movie> GetAllMovies();
        public Movie GetMovieById(int id);
        public void CreateMovie(Movie mv);
        public void UpdateMovie(Movie mv);
        public void DeleteMovie(Movie mv);
        public bool SaveChanges();

    }
}
