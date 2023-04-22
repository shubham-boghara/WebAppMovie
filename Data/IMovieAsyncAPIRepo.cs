using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppMovie.Models;

namespace WebAppMovie.Data
{
    public interface IMovieAsyncAPIRepo
    {
         Task<IEnumerable<Movie>> GetAsyncAllMovies();
         Task<Movie> GetAsyncMovieById(int id);
         Task CreatAsyncMovie(Movie mv);
         void UpdateAsyncMovie(Movie mv);
         void DeleteAsyncMovie(Movie mv);
         Task<bool> SaveAsyncChanges();
    }
}
