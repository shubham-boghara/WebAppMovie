using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAppMovie.Auth;
using WebAppMovie.Data;
using WebAppMovie.Filters;

namespace WebAppMovie.Controllers.Auth
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomUserManager customUserManager;
        private readonly ICustomTokenManager customTokenManager;
        private readonly IUserAsyncAPIRepo userAsyncAPIRepo;

        public AuthController(ICustomUserManager customUserManager,ICustomTokenManager customTokenManager,IUserAsyncAPIRepo userAsyncAPIRepo) {
            this.customUserManager = customUserManager;
            this.customTokenManager = customTokenManager;
            this.userAsyncAPIRepo = userAsyncAPIRepo;
        }
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(string userName,string email,string password)
        {
            var user = userAsyncAPIRepo.GetUserByEmailAndPassword(email, password);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(await Task.FromResult(customUserManager.Authenticate(user.Result.UserName, user.Result.Email ,user.Result.Password, user.Result.UserId)));
        }

        [HttpGet]
        [Route("verifytoken")]
        public async Task<bool> Verify(string token)
        {
            return await Task.FromResult(customTokenManager.VerifyToken(token));
        }

        [HttpGet]
        [Route("getuserinfo")]
        public async Task<PlayLoad> GetUserInfoByToken(string token)
        {
            return await Task.FromResult(customTokenManager.GetUserInfoByToken(token));
        }
    }
}
