using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.AdminPanel;

namespace Store.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public HomeController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        public IActionResult Index(string searchValue, int page=1, int pageSize = 4,int isDel = 0)
        {
            var model = _userService.GetUsers(searchValue,page,pageSize, isDel);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("~/areas/admin/Views/home/_gridUser.cshtml", model);
            }
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.RoleName = _permissionService.GetRoles().roleList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserDto create)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoleName = _permissionService.GetRoles().roleList;
                return View(create);
            }
            _userService.AddUserForm(create);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var model = _userService.GetUserForDetailsAdmin(id);
            ViewBag.RoleName = _permissionService.GetRoles().roleList;
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var model = _userService.GetUserForEditAdmin(id);
            ViewBag.RoleName = _permissionService.GetRoles().roleList;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditUserDto edit)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoleName = _permissionService.GetRoles().roleList;
                return View(edit);
            }
            _userService.EditUserAdmin(edit);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var model = _userService.GetUserForDeleteAdmin(id);
            ViewBag.RoleName = _permissionService.GetRoles().roleList;
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteUserDto delete)
        {
            _userService.DeleteUserAdmin(delete);
            return RedirectToAction("Index");
        }
    }
}
