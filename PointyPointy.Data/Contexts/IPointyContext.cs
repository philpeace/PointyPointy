using System.Data.Entity;
using PointyPointy.Data.Entities;

namespace PointyPointy.Data.Contexts
{
    public interface IPointyContext : IDbContext
    {
        IDbSet<ScrumInvite> ScrumInvite { get; set; }

        IDbSet<ScrumInviteUser> ScrumInviteUser { get; set; }
    }
}