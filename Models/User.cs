﻿using System.ComponentModel.DataAnnotations;

namespace WebAppMovie.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        
        public string Eamil { get; set; }

        public string Password { get; set; }
    }
}
