using System;

namespace PointyPointy.Data.Entities
{
    public class ScrumInviteUser
    {
        public int Id { get; set; }

        public ScrumInvite Invite { get; set; }

        public string Email { get; set; }

        public DateTime? Responded { get; set; }

        public bool Accepted { get; set; }

        public string RequestKey { get; set; }
    }
}
