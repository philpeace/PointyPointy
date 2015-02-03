using CodePeace.Common;

namespace PointyPointy.Services
{
    public interface IApplicationUserManagerFactory : IDependency
    {
        ApplicationUserManager Create();
    }
}