using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppMovie.Models;

namespace WebAppMovie.Data
{
    public interface IUserAsyncAPIRepo
    {
        Task<IEnumerable<User>> GetAsyncAllUsers();
        Task<User> GetAsyncUserById(int id);
        Task CreatAsyncUser(User user);
        void UpdateAsyncUser(User user);
        void DeleteAsyncUser(User user);
        Task<bool> SaveAsyncChanges();
        Task<User> GetUserByEmailAndPassword(string email, string password);
    }
}
