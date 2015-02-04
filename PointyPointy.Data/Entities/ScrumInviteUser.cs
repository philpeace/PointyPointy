using System;

namespace PointyPointy.Data.Entities
{
    public class ScrumInviteUser
    {
        public int Id { get; set; }

        public ScrumInvite ScrumInvite { get; set; }

        public string UserId { get; set; }

        public DateTime? Responded { get; set; }

        public bool Accepted { get; set; }
    }
}
