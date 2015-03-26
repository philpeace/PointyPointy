using System;
using System.Net.Mail;
using System.Threading.Tasks;
using CodePeace.Common;
using CodePeace.Common.Configuration;

namespace PointyPointy.Services
{
    public class EmailService : IEmailService
    {
        private IAppSettingsConfigurationManager _appSettingsConfigurationManager;

        public EmailService(IAppSettingsConfigurationManager appSettingsConfigurationManager)
        {
            _appSettingsConfigurationManager = appSettingsConfigurationManager;
        }

        public ServiceResponse Send(MailMessage message)
        {
            var response = new ServiceResponse();

            try
            {
                using (var smtp = CreateClient())
                {
                    smtp.Send(message);
                }
            }
            catch (Exception e)
            {
                response.AddError(e.Message);
            }
            
            return response;
        }

        public async Task<ServiceResponse> SendAsync(MailMessage message)
        {
            var response = new ServiceResponse();

            try
            {
                using (var smtp = CreateClient())
                {
                    var token = new object();
                    await Task.Run(() => smtp.SendAsync(message, token));
                    
                }
            }
            catch (Exception e)
            {
                response.AddError(e.Message);
            }

            return response;
        }

        private SmtpClient CreateClient()
        {
            var host = _appSettingsConfigurationManager.Setting("Email.Host");

            var client = new SmtpClient(host);

            return client;
        }
    }
}
