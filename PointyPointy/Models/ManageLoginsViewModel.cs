using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace PointyPointy.Models
{
    public class ManageLoginsViewModel
    {
        public ManageLoginsViewModel()
        {
            LoginProviders = Enumerable.Empty<AuthenticationDescription>();
        }

        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationDescription> OtherLogins { get; set; }

        public IEnumerable<AuthenticationDescription> LoginProviders { get; set; }
    }
}