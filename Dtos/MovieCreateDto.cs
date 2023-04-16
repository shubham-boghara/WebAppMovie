using System.ComponentModel.DataAnnotations;

namespace WebAppMovie.Dtos
{
    public class MovieCreateDto
    {
        [Required]
        public string Title { get; set; }
    }
}
