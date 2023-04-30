using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppMovie.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Otp { get; set; }

        public string ExpireOn { get; set; }

        public bool? EmailVerified { get; set; }    

        public DateTime? CreatedOn { get; set; } 
    }
}
