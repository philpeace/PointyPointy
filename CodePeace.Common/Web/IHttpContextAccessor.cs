using System.Web;

namespace CodePeace.Common.Web
{
    public interface IHttpContextAccessor
    {
        HttpContextBase Current();
    }
}