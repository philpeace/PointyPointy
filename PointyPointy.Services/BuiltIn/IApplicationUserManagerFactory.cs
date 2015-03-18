using CodePeace.Common;

namespace PointyPointy.Services.BuiltIn
{
    public interface IApplicationUserManagerFactory : IDependency
    {
        ApplicationUserManager Create();
    }
}