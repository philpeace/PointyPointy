using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PointyPointy.Services
{
    public class MailMessageBuilder : IMailMessageBuilder
    {
        public MailMessage Build(string to, string from, string subject, string body, bool isHtml)
        {
            var msg = new MailMessage(from, to);

            msg.Subject = subject;
            msg.Body = body;
            msg.IsBodyHtml = isHtml;

            return msg;
        }
    }
}
