using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppMovie.Data;
using WebAppMovie.Models;

namespace WebAppMovie.ApiRepository.cs
{
    public class MovieAPIRepo : IMovieAsyncAPIRepo
    {
        private readonly MovieDbContext _DB;
        public MovieAPIRepo(MovieDbContext DB) {
          _DB = DB;
        }

        public void DeleteAsyncMovie(Movie mv)
        {
            if (mv == null)
            {
                throw new ArgumentNullException(nameof(mv));
            }

             _DB.Remove(mv);
            
        }


        public async Task<Movie> GetAsyncMovieById(int id)
        {
            var get_movie = await _DB.Movies.FirstOrDefaultAsync(c => c.MovieId == id);

            return get_movie;
        }

        public void UpdateAsyncMovie(Movie mv)
        {

           
        }

        public async Task<bool>  SaveAsyncChanges()
        {
            return (await _DB.SaveChangesAsync() >= 0);
        }

        public async Task<IEnumerable<Movie>> GetAsyncAllMovies()
        {
            var movies = await _DB.Movies.ToListAsync();
            return movies;
        }

        public async Task CreatAsyncMovie(Movie mv)
        {
            if (mv == null)
            {
                throw new ArgumentNullException(nameof(mv));
            }
            await _DB.Movies.AddAsync(mv);
        }
    }
}
