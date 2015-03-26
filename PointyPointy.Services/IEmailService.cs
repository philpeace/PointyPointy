using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CodePeace.Common;

namespace PointyPointy.Services
{
    public interface IEmailService : IDependency
    {
        ServiceResponse Send(MailMessage message);

        Task<ServiceResponse> SendAsync(MailMessage message);
    }
}
