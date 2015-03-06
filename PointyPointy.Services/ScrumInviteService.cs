using System;
using System.Linq;
using PointyPointy.Data;
using PointyPointy.Data.Entities;
using CodePeace.Common.Extensions;

namespace PointyPointy.Services
{
    public class ScrumInviteService : IScrumInviteService
    {
        private readonly IRepository<ScrumInvite> _scrumInviteRepository;
        private readonly IRepository<ScrumInviteUser> _scrumInviteUserRepository;

        public ScrumInviteService(IRepository<ScrumInvite> scrumInviteRepository, IRepository<ScrumInviteUser> scrumInviteUserRepository)
        {
            _scrumInviteRepository = scrumInviteRepository;
            _scrumInviteUserRepository = scrumInviteUserRepository;
        }

        public ScrumInvite CreateInviteForUsers(string userId, string email, string[] users)
        {
            if (users != null && users.Any())
            {
                var invite = _scrumInviteRepository.Create(new ScrumInvite { UserId = userId });

                foreach (var user in users)
                {
                    var inviteKey = string.Format("{0}-{1}-{2}", invite.Id, email, DateTime.Now.Ticks).ToSha256();

                    _scrumInviteUserRepository.Create(new ScrumInviteUser { Invite = invite, Email = user, RequestKey = inviteKey });
                }

                return invite;
            }

            return null;
        }

        public ScrumInviteUser GetInviteUserForKey(string key, string email)
        {
            return _scrumInviteUserRepository.Fetch(i => i.RequestKey == key && i.Email == email).FirstOrDefault();
        }

        public ScrumInviteUser Respond(int id, string email, bool accept)
        {
            var invite = _scrumInviteUserRepository.Get(i => i.Invite.Id == id && i.Email == email);

            invite.Accepted = accept;
            invite.Responded = DateTime.Now;

            invite = _scrumInviteUserRepository.Update(invite);

            return invite;
        }

        public ScrumInvite GetById(int id)
        {
            return _scrumInviteRepository.Get(id);
        }
    }
}