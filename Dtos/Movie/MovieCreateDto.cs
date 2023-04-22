using System.ComponentModel.DataAnnotations;

namespace WebAppMovie.Dtos.Movie
{
    public class MovieCreateDto
    {
        [Required]
        public string Title { get; set; }
    }
}
