using CodePeace.Common;
using PointyPointy.Data.Entities;
using PointyPointy.Services.BuiltIn.Models;

namespace PointyPointy.Services
{
    public interface IScrumInviteService : IDependency
    {
        ServiceResponse<ScrumInvite> CreateInviteForUsers(ApplicationUser invitee, string[] users);

        ServiceResponse<ScrumInviteUser> GetInviteUserForKey(string key, string email);

        ServiceResponse<ScrumInviteUser> Respond(int id, string email, string key, bool accept);

        ServiceResponse<ScrumInvite> GetById(int id);
    }
}