using CodePeace.Common;
using PointyPointy.Data.Entities;

namespace PointyPointy.Services
{
    public interface IScrumInviteService : IDependency
    {
        ScrumInvite CreateInviteForUsers(string userId, string email, string[] users);

        ScrumInviteUser Respond(int id, string email, bool accept);
    }
}