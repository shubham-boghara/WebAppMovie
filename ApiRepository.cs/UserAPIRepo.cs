using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task CreatAsyncUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _DB.AddAsync(user);

        }

        public void DeleteAsyncUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _DB.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAsyncAllUsers()
        {
            return await _DB.Users.ToListAsync();
        }

        public async Task<User> GetAsyncUserById(int id)
        {
            return await _DB.Users.SingleOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<bool> SaveAsyncChanges()
        {
            return (await _DB.SaveChangesAsync() >= 0);
        }

        public void UpdateAsyncUser(User user)
        {
           
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            return await (_DB.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password));
        }
    }
}
