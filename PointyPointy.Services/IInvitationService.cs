using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodePeace.Common;
using PointyPointy.Data.Entities;
using PointyPointy.Services.BuiltIn.Models;

namespace PointyPointy.Services
{
    public interface IInvitationService : IDependency
    {
        ServiceResponse SendInvite(ScrumInviteUser inviteUser, ApplicationUser fromUser);
    }
}
