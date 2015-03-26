using System;
using System.Linq;
using PointyPointy.Data;
using PointyPointy.Data.Entities;
using CodePeace.Common.Extensions;
using PointyPointy.Services.BuiltIn.Models;
using CodePeace.Common;

namespace PointyPointy.Services
{
    public class ScrumInviteService : IScrumInviteService
    {
        private readonly IRepository<ScrumInvite> _scrumInviteRepository;
        private readonly IRepository<ScrumInviteUser> _scrumInviteUserRepository;
        private readonly IInvitationService _invitationService;

        public ScrumInviteService(IRepository<ScrumInvite> scrumInviteRepository, IRepository<ScrumInviteUser> scrumInviteUserRepository, IInvitationService invitationService)
        {
            _scrumInviteRepository = scrumInviteRepository;
            _scrumInviteUserRepository = scrumInviteUserRepository;
            _invitationService = invitationService;
        }

        public ServiceResponse<ScrumInvite> CreateInviteForUsers(ApplicationUser invitee, string[] users)
        {
            var response = new ServiceResponse<ScrumInvite>();

            if (users != null && users.Any())
            {
                var invite = _scrumInviteRepository.Create(new ScrumInvite { UserId = invitee.Id });

                foreach (var user in users)
                {
                    var inviteKey = string.Format("{0}-{1}-{2}", invite.Id, invitee.Email, DateTime.Now.Ticks).ToSha256();

                    var newUser = _scrumInviteUserRepository.Create(new ScrumInviteUser { Invite = invite, Email = user, RequestKey = inviteKey });
                    invite.Users.Add(newUser);

                    var inviteResponse = _invitationService.SendInvite(newUser, invitee);

                    if (!inviteResponse.Success)
                    {
                        response.AddErrors(inviteResponse.Errors);
                    }
                }

                response.Response = invite;

                return response;
            }

            response.AddError("No invites specified");

            return response;
        }

        public ServiceResponse<ScrumInviteUser> GetInviteUserForKey(string key, string email)
        {
            var response = new ServiceResponse<ScrumInviteUser>();

            try
            {
                response.Response = _scrumInviteUserRepository.Fetch(i => i.RequestKey == key && i.Email == email).FirstOrDefault();
            }
            catch (Exception e)
            {
                response.AddError(e.Message);
            }

            return response;
        }

        public ServiceResponse<ScrumInviteUser> Respond(int id, string email, string key, bool accept)
        {
            var response = new ServiceResponse<ScrumInviteUser>();

            var invite = _scrumInviteUserRepository.Get(i => i.Invite.Id == id && i.Email == email && i.RequestKey == key);

            if (invite == null)
            {
                response.AddError("Invite not found");
            }
            else
            {
                invite.Accepted = accept;
                invite.Responded = DateTime.Now;

                invite = _scrumInviteUserRepository.Update(invite);

                response.Response = invite;
            }

            return response;
        }

        public ServiceResponse<ScrumInvite> GetById(int id)
        {
            return new ServiceResponse<ScrumInvite>(_scrumInviteRepository.Get(id));
        }
    }
}