using System;
using Moq;
using PointyPointy.Data;
using PointyPointy.Data.Entities;
using PointyPointy.Services;
using Xunit;

namespace PointyPointy.Tests.Services
{
    public class ScrumInviteServiceTests
    {
        [Fact]
        public void ConstructorReturnsNotNull()
        {
            var inviteRepo = new Mock<IRepository<ScrumInvite>>();
            var inviteUserRepo = new Mock<IRepository<ScrumInviteUser>>();

            var target = new ScrumInviteService(inviteRepo.Object, inviteUserRepo.Object, null);

            Assert.NotNull(target);
        }
    }
}
