using Microsoft.AspNet.Identity.EntityFramework;

namespace PointyPointy.Services.BuiltIn.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("PointyContext", false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}