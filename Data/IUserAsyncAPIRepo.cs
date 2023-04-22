using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppMovie.Models;

namespace WebAppMovie.Data
{
    public interface IUserAsyncAPIRepo
    {
        Task<IEnumerable<Movie>> GetAsyncAllUsers();
        Task<Movie> GetAsyncUserById(int id);
        Task CreatAsyncUser(Movie mv);
        void UpdateAsyncUser(Movie mv);
        void DeleteAsyncUser(Movie mv);
        Task<bool> SaveAsyncChanges();
    }
}
