using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppMovie.Data;
using WebAppMovie.Models;

namespace WebAppMovie.ApiRepository.cs
{
    public class UserAPIRepo : IUserAsyncAPIRepo
    {
        private readonly MovieDbContext _DB;
        public UserAPIRepo(MovieDbContext dB)
        {
            _DB = dB;
        }
        public Task CreatAsyncUser(Movie mv)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAsyncUser(Movie mv)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetAsyncAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public Task<Movie> GetAsyncUserById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAsyncChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAsyncUser(Movie mv)
        {
            throw new System.NotImplementedException();
        }
    }
}
