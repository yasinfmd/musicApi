using MusicApp.Helper.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MusicApp.Helper.Concrate
{
    public class SendGridMailService : IMailService
    {
        private IConfiguration _configuration;

        //public SendGridMailService(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public  async Task SendEmail(string toEmail, string subject, string content)
        {
            var apiKey = "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";
                //_configuration["SendGridApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ysndlklc1234@gmail.com", "MusicApp Api");
            var to = new EmailAddress(toEmail, toEmail);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
