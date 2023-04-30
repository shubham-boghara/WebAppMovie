using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using OtpNet;
using System;
using System.Text;
using System.Threading.Tasks;
using WebAppMovie.ApiResponse;
using WebAppMovie.Auth;
using WebAppMovie.Data;
using WebAppMovie.Models;

namespace WebAppMovie.Controllers.Auth
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomUserManager customUserManager;
        private readonly ICustomTokenManager customTokenManager;
        private readonly IUserAsyncAPIRepo userAsyncAPIRepo;
        private readonly IAppResponse appResponse;
        private readonly ICustomEmailManager customEmailManager;
        private readonly MovieDbContext DB;

        public AuthController(ICustomUserManager customUserManager,
            ICustomTokenManager customTokenManager,
            IUserAsyncAPIRepo userAsyncAPIRepo,
            IAppResponse appResponse,
            ICustomEmailManager customEmailManager,
            MovieDbContext movieDbContext) {
            this.customUserManager = customUserManager;
            this.customTokenManager = customTokenManager;
            this.userAsyncAPIRepo = userAsyncAPIRepo;
            this.appResponse = appResponse;
            this.customEmailManager = customEmailManager;
            this.DB = movieDbContext;
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

        [HttpPost]
        [Route("sendemail")]
        public async Task<bool> SendEmail(string token)
        {
            try
            {
                var verifyToken = await Task.FromResult(customTokenManager.VerifyToken(token));
                if (!verifyToken)
                {
                    return false;
                }
                var getUserInfoByToken = await Task.FromResult(customTokenManager.GetUserInfoByToken(token));

                var otpProvider = new Totp(Encoding.UTF8.GetBytes("your-secret-key-here"));
                var otp = otpProvider.ComputeTotp();

                var verification = new EmailVerification
                {
                    UserId = getUserInfoByToken.UserId,
                    Email = getUserInfoByToken.Email,
                    Otp = otp,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(2) // OTP expires in 1 minutes
                };

                DB.Add(verification);
                await DB.SaveChangesAsync();

                var mm = new MMessage();
                mm.Subject = "Email Verification";
                mm.To = getUserInfoByToken.Email;
                mm.body = "Your OTP is " + otp;

                var sendEmail = await customEmailManager.SendEmailToUser(mm);

                return sendEmail;
            }
            catch(Exception ex)
            {
                return await Task.FromResult(false);
            }

        }

        [HttpPost]
        [Route("verifyemail")]
        public async Task<bool> VerifyEmail(string token,string otp)
        {
            try
            {
                var verifyToken = await Task.FromResult(customTokenManager.VerifyToken(token));
                if (!verifyToken)
                {
                    return false;
                }
                var getUserInfoByToken = await Task.FromResult(customTokenManager.GetUserInfoByToken(token));

                // Find the email verification record in your database
                var verification = await DB.EmailVerifications.FirstOrDefaultAsync(v => v.Email == getUserInfoByToken.Email && v.Otp == otp && v.ExpiresAt >= DateTime.UtcNow);

                if (verification != null)
                {
                    // Update the user's account status to indicate that their email address is verified
                    var user = await DB.Users.FirstOrDefaultAsync(u => u.Email == getUserInfoByToken.Email && u.UserId == getUserInfoByToken.UserId);
                    if (user != null)
                    {
                        user.EmailVerified = true;
                        
                        return await DB.SaveChangesAsync() >= 0;
                    }

                    // Remove the email verification record from your database
                    // dbContext.EmailVerifications.Remove(verification);
                    // dbContext.SaveChanges();
                }
                return await Task.FromResult(false);
            }
            catch(Exception ex)
            {
                return await Task.FromResult(false);
            }

            
        }
    }
}
