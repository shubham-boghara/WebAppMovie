using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAppMovie.ApiResponse;
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
        private readonly IAppResponse appResponse;

        public AuthController(ICustomUserManager customUserManager,
            ICustomTokenManager customTokenManager,
            IUserAsyncAPIRepo userAsyncAPIRepo,
            IAppResponse appResponse) {
            this.customUserManager = customUserManager;
            this.customTokenManager = customTokenManager;
            this.userAsyncAPIRepo = userAsyncAPIRepo;
            this.appResponse = appResponse;
        }
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(string email,string password)
        {
            if (string.IsNullOrWhiteSpace(email)) appResponse.AddValidationError(new ApiValidationError { Field="email", Message="The Email field is requried." });

            if (string.IsNullOrWhiteSpace(password)) appResponse.AddValidationError(new ApiValidationError { Field= "password", Message="The Password filed is requried." });

            if (!appResponse.IsValid())
            {
                return BadRequest(appResponse.ValidationErrors());
            }
            
            var user = await userAsyncAPIRepo.GetUserByEmailAndPassword(email, password);
            if(user == null)
            {
                appResponse.AddValidationError(new ApiValidationError { Field = "", Message = "Invalid Email or Password." });
            }
            if (!appResponse.IsValid())
            {
                return NotFound(appResponse.ValidationErrors());
            }
            return Ok(appResponse.Write(await Task.FromResult(customUserManager.Authenticate(user.UserName, user.Email ,user.Password, user.UserId))));
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
