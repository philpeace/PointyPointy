using System.Web.Mvc;

namespace PointyPointy.Controllers
{
    [Authorize]
    public class InviteController : Controller
    {
        // GET: Invite
        public ActionResult Index()
        {
            return View();
        }
    }
}