using System;

namespace PointyPointy.Data.Entities
{
    public class ScrumInviteUser
    {
        public int Id { get; set; }

        public int ScrumInviteId { get; set; }

        public int UserId { get; set; }

        public DateTime Responded { get; set; }

        public bool Accepted { get; set; }
    }
}
