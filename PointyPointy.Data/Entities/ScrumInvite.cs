using System;
using System.Collections.Generic;

namespace PointyPointy.Data.Entities
{
    public class ScrumInvite
    {
        public ScrumInvite()
        {
            Created = DateTime.Now;
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime Created { get; set; }

        public IList<ScrumInviteUser> Users { get; set; }
    }
}