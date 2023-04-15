using Microsoft.EntityFrameworkCore;
using WebAppMovie.Models;

namespace WebAppMovie.Data
{
    public class MovieDbContext: DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options )
            :base(options)
        {
           
        }

       public DbSet<Movie> Movies { get; set; }
}
}
