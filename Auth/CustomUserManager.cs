using System.Collections.Generic;
using WebAppMovie.Data;

namespace WebAppMovie.Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        /*private Dictionary<string, string> credentials = new Dictionary<string, string>()
        {
            { "shubham","password" }
        };*/
        private readonly ICustomTokenManager customTokenManager;
        

        public CustomUserManager(ICustomTokenManager customTokenManager) { 
          this.customTokenManager = customTokenManager;
          
        }
        public string Authenticate(string userName, string email ,string password, int id)
        {
            //validate the credentials;
            /* if (credentials[userName] != password){
                 return string.Empty;    
             }*/
            if(string.IsNullOrWhiteSpace(userName) || id == 0) {
                return string.Empty;
            }

            var playLoad = new PlayLoad();
            playLoad.UserName = userName;
            playLoad.UserId = id;
            //generate token;
            return customTokenManager.CreateToken(playLoad);
        }

       
    }
}
