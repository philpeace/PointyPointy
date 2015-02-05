using CodePeace.Common;
using PointyPointy.Data.Entities;

namespace PointyPointy.Services
{
    public interface IScrumInviteService : IDependency
    {
        ScrumInvite CreateInviteForUsers(string userId, string email, string[] users);

        ScrumInviteUser GetInviteUserForKey(string key, string email);

        ScrumInviteUser Respond(int id, string email, bool accept);
    }
}