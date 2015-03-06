using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PointyPointy.Data;
using PointyPointy.Data.Entities;
using PointyPointy.Models;
using PointyPointy.Services;
using PointyPointy.ViewModels;

namespace PointyPointy.Controllers
{
    public class InviteController : Controller
    {
        private readonly IScrumInviteService _scrumInviteService;

        public InviteController(IScrumInviteService scrumInviteService)
        {
            _scrumInviteService = scrumInviteService;
        }

        // GET: Invite
        [Authorize]
        public ActionResult Index(InviteViewModel vm)
        {
            return View(vm);
        }

        // GET: Invite/Create
        [Authorize]
        public ActionResult Create()
        {
            var vm = new InviteCreateViewModel();

            return View(vm);
        }

        // POST: Invite/Create
        [HttpPost]
        [Authorize]
        [ActionName("Create")]
        public ActionResult CreatePOST(InviteCreateViewModel vm)
        {
            var invites = vm.Invitees.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var invite = _scrumInviteService.CreateInviteForUsers(vm.User.Id, vm.User.Email, invites);

            return RedirectToAction("Created", new { id = invite.Id });
        }

        public ActionResult Created(InviteCreatedViewModel vm)
        {
            var invite = _scrumInviteService.GetById(vm.Id);

            if (invite.UserId != vm.User.Id)
            {
                return new HttpUnauthorizedResult();
            }

            return View(vm);
        }

        // GET: Invite/Respond
        public ActionResult Respond(InviteRespondViewModel vm)
        {
            return View(vm);
        }

        // POST: Invite/Response
        [HttpPost]
        [Authorize]
        public ActionResult Respond(InviteResponseViewModel vm)
        {
            vm.InviteUser = _scrumInviteService.Respond(vm.Id, vm.Email, vm.Accept);

            return View(vm);
        }
    }
}