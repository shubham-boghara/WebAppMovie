using System.Net.Mail;
using System.Net;
using System;
using System.Threading.Tasks;

namespace WebAppMovie.Auth
{
    public class CustomEmailManager : ICustomEmailManager
    {
        public async Task<bool> SendEmailToUser(MMessage mailMessage)
        {
            bool sendEmail = false;
            try
            {
                using (var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525))
                {
                    MailMessage mm = new MailMessage();
                    mm.From = new MailAddress("shubhamboghara63@outlook.com");
                    mm.To.Add(mailMessage.To);
                    mm.Subject = mailMessage.Subject;
                    mm.Body = mailMessage.body;

                    
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("2ff18f0a6477b0", "841569cdf47e81");
                    await client.SendMailAsync(mm);
                    
                    sendEmail = true;

                }
                
            }
            catch(Exception ex)
            {
                sendEmail = false;
            }
            return sendEmail;
        }
    }
}
