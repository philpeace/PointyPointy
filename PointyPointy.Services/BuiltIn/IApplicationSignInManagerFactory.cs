using CodePeace.Common;

namespace PointyPointy.Services.BuiltIn
{
    public interface IApplicationSignInManagerFactory : IDependency
    {
        ApplicationSignInManager Create();
    }
}