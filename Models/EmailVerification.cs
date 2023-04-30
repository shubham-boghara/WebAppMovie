using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppMovie.Models
{
    public class EmailVerification
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Otp { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int? UserId { get; set; }
    }
}