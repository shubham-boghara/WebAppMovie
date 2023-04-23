using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WebAppMovie.Auth
{
    public class JwtTokenManager : ICustomTokenManager
    {
        private JwtSecurityTokenHandler tokenHandler;
        private readonly IConfiguration configuration;
        private byte[] secrectKey; 
        public JwtTokenManager(IConfiguration configuration)
        {
            this.configuration = configuration;
            tokenHandler = new JwtSecurityTokenHandler();
            secrectKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("jwtSecrectKey"));
        }
        public string CreateToken(PlayLoad playLoad)
        {
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                     new Claim[]
                     {
                         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                         new Claim(ClaimTypes.Name,playLoad.UserName),
                         new Claim(JwtRegisteredClaimNames.NameId,playLoad.UserId.ToString())
                         
                     }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secrectKey),
                    SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
            

            
        }

        public PlayLoad GetUserInfoByToken(string token)
        {
            if(string.IsNullOrEmpty(token)) return null;

            var jwtToken = tokenHandler.ReadToken(token.Replace("\"",string.Empty)) as JwtSecurityToken;

            var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name");
            var claimId = jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid");
            var playLoad = new PlayLoad();
            playLoad.UserName = claim.Value;
            playLoad.UserId = Convert.ToInt32(claimId.Value);
            if(claim != null) return playLoad;

            return null;
        }

        public bool VerifyToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;

            SecurityToken securityToken;

            try
            {
               tokenHandler.ValidateToken(
               token.Replace("\"", string.Empty),
               new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(secrectKey),
                   ValidateLifetime = true,
                   ValidateAudience = false,
                   ValidateIssuer = false,
                   ClockSkew = TimeSpan.Zero
               },
               out securityToken);
            }
            catch (SecurityTokenException ex)
            {
                return false;
            }catch(Exception ex)
            {
                return false;
            }

            return securityToken != null;
        }
    }
}
