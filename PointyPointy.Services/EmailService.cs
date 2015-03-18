using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace PointyPointy.Services
{
    public class EmailService : IEmailService
    {
        public void Send(MailMessage message)
        {
            var smtp = new SmtpClient();

            // Do other stuff here

            smtp.Send(message);
        }
    }
}
