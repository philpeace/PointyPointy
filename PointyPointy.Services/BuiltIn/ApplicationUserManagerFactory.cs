using System.Web;
using CodePeace.Common.Web;
using Microsoft.AspNet.Identity.Owin;

namespace PointyPointy.Services.BuiltIn
{
    public class ApplicationUserManagerFactory : IApplicationUserManagerFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUserManagerFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ApplicationUserManager Create()
        {
            return _httpContextAccessor.Current().GetOwinContext().GetUserManager<ApplicationUserManager>();
        }
    }
}