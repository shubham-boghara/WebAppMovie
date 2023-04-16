using System.ComponentModel.DataAnnotations;

namespace WebAppMovie.Dtos
{
    public class MovieUpdateDto
    {
        [Required]
        public string Title { get; set; }
    }
}
