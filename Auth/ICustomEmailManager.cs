using System.Threading.Tasks;

namespace WebAppMovie.Auth
{
    public interface ICustomEmailManager
    {
        Task<bool> SendEmailToUser(MMessage mailMessage);

        //bool VerifyEmail(string otp,string email);
    }
}
