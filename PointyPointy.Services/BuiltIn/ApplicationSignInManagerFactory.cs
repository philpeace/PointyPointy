using System.Web;
using CodePeace.Common.Web;
using Microsoft.AspNet.Identity.Owin;

namespace PointyPointy.Services.BuiltIn
{
    public class ApplicationSignInManagerFactory : IApplicationSignInManagerFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationSignInManagerFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ApplicationSignInManager Create()
        {
            return _httpContextAccessor.Current().GetOwinContext().Get<ApplicationSignInManager>();
        }
    }
}