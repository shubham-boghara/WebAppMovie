using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace WebAppMovie.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
