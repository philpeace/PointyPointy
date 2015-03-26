using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using CodePeace.Common;

namespace PointyPointy.Services
{
    public interface IMailMessageBuilder : IDependency
    {
        MailMessage Build(string to, string from, string subject, string body, bool isHtml);
    }
}
