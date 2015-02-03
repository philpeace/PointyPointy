using CodePeace.Common;

namespace PointyPointy.Services
{
    public interface IApplicationSignInManagerFactory : IDependency
    {
        ApplicationSignInManager Create();
    }
}