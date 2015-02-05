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
    [Authorize]
    public class InviteController : Controller
    {
        private readonly IScrumInviteService _scrumInviteService;

        public InviteController(IScrumInviteService scrumInviteService)
        {
            _scrumInviteService = scrumInviteService;
        }

        // GET: Invite
        public ActionResult Index(InviteViewModel vm)
        {
            return View(vm);
        }

        // GET: Invite/Respond
        public ActionResult Respond(InviteRespondViewModel vm)
        {
            

            return View(vm);
        }

        // POST: Invite/Response
        [HttpPost]
        public ActionResult Response(InviteResponseViewModel vm)
        {
            vm.InviteUser = _scrumInviteService.Respond(vm.Id, vm.Email, vm.Accept);

            return View(vm);
        }

        // POST: Invite/Create
        [HttpPost]
        public ActionResult Create(InviteCreateViewModel vm)
        {
            _scrumInviteService.CreateInviteForUsers(vm.User.Id, vm.User.Email, vm.Invitees.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

            return View(vm);
        }
    }
}