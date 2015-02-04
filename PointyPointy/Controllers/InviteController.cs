using System.Web.Mvc;
using PointyPointy.Data;
using PointyPointy.Data.Entities;
using PointyPointy.Models;

namespace PointyPointy.Controllers
{
    [Authorize]
    public class InviteController : Controller
    {
        private IRepository<ScrumInvite> _scrumInviteRepository;

        public InviteController(IRepository<ScrumInvite> scrumInviteRepository)
        {
            _scrumInviteRepository = scrumInviteRepository;
        }

        // GET: Invite
        public ActionResult Index()
        {
            var vm = new InviteIndexViewModel();

            return View(vm);
        }
    }
}