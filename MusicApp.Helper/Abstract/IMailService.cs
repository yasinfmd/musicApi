using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Helper.Abstract
{
    public interface IMailService
    {
        Task SendEmail(string toEmail, string subject, string content);
    }
}
