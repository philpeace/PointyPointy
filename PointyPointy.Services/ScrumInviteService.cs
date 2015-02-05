using System;
using System.Linq;
using PointyPointy.Data;
using PointyPointy.Data.Entities;

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
                    _scrumInviteUserRepository.Create(new ScrumInviteUser { Invite = invite, Email = user });
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
    }
}