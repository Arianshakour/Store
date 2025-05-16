using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.CustomAutorize;
using Store.Application.Services.Interfaces;
using Store.Domain.Common.Utilities;
using Store.Domain.Dtoes.AdminPanel;

namespace Store.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[PermissionChecker(2)]
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
        //[PermissionChecker(3)]
        public IActionResult Create()
        {
            ViewBag.RoleName = _permissionService.GetRoles().roleList;
            return PartialView();
        }
        [HttpPost]
        public IActionResult Create(CreateUserDto create)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoleName = _permissionService.GetRoles().roleList;
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Areas/Admin/Views/Home/Create.cshtml", create);
                return Json(new { view = v1, success = false });
                //return View(create);
            }
            _userService.AddUserForm(create);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var model = _userService.GetUserForDetailsAdmin(id);
            ViewBag.RoleName = _permissionService.GetRoles().roleList;
            return PartialView(model);
        }
        public IActionResult Edit(int id)
        {
            var model = _userService.GetUserForEditAdmin(id);
            ViewBag.RoleName = _permissionService.GetRoles().roleList;
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult Edit(EditUserDto edit)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoleName = _permissionService.GetRoles().roleList;
                //return View(edit);
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Areas/Admin/Views/Home/Edit.cshtml", edit);
                return Json(new { view = v1, success = false });
            }
            _userService.EditUserAdmin(edit);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var model = _userService.GetUserForDeleteAdmin(id);
            ViewBag.RoleName = _permissionService.GetRoles().roleList;
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteUserDto delete)
        {
            _userService.DeleteUserAdmin(delete);
            return RedirectToAction("Index");
        }
    }
}
