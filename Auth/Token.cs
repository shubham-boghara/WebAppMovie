using System;

namespace WebAppMovie.Auth
{
    public class Token
    {
        public Token(string userName) { 
          this.UserName = userName;
            this.TokenString = Guid.NewGuid().ToString();
            this.ExpiryDate = DateTime.Now.AddMinutes(1);
        }
        public string TokenString { get; set; }

        public string UserName { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
