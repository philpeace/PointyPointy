using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PointyPointy.Data;
using PointyPointy.Data.Entities;
using PointyPointy.Models;
using PointyPointy.Services;

namespace PointyPointy.Controllers
{
    [Authorize]
    public class InviteController : Controller
    {
        private readonly IRepository<ScrumInvite> _scrumInviteRepository;
        private ApplicationUserManager _applicationUserManager;

        public InviteController(IRepository<ScrumInvite> scrumInviteRepository, IApplicationUserManagerFactory applicationUserManagerFactory)
        {
            _scrumInviteRepository = scrumInviteRepository;

            _applicationUserManager = applicationUserManagerFactory.Create();
        }

        // GET: Invite
        public async Task<ActionResult> Index()
        {
            var vm = new InviteIndexViewModel();

            var user = await _applicationUserManager.FindByIdAsync(User.Identity.GetUserId());

            var s = _scrumInviteRepository.Create(new ScrumInvite { UserId = user.Id });

            return View(vm);
        }
    }
}