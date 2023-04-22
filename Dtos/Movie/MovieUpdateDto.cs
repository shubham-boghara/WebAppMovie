using System.ComponentModel.DataAnnotations;

namespace WebAppMovie.Dtos.Movie
{
    public class MovieUpdateDto
    {
        [Required]
        public string Title { get; set; }
    }
}
