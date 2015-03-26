using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodePeace.Common;
using CodePeace.Common.Configuration;
using PointyPointy.Data.Entities;
using PointyPointy.Services.BuiltIn;
using PointyPointy.Services.BuiltIn.Models;

namespace PointyPointy.Services
{
    public class InvitationService : IInvitationService
    {
        private IEmailService _emailService;
        private IMailMessageBuilder _mailMessageBuilder;

        public InvitationService(IEmailService emailService, IMailMessageBuilder mailMessageBuilder, IApplicationUserManagerFactory applicationUserManagerFactory)
        {
            _emailService = emailService;
            _mailMessageBuilder = mailMessageBuilder;
        }

        public ServiceResponse SendInvite(ScrumInviteUser inviteUser, ApplicationUser fromUser)
        {
            var subject = "PointyPointy Invite";
            var body = "You're Invited";

            var message = _mailMessageBuilder.Build(fromUser.Email, fromUser.Email, subject, body, false);

            return _emailService.Send(message);
        }
    }
}
