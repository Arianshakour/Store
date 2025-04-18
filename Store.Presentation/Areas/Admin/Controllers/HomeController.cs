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
        public IActionResult Index(string searchValue, int page=1, int pageSize = 4)
        {
            var model = _userService.GetUsers(searchValue,page,pageSize);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.RoleName = _permissionService.GetRole().roleList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserDto create)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.RoleName = _permissionService.GetRole().roleList;
                return View(create);
            }
            _userService.AddUserForm(create);
            return RedirectToAction("Index");
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Edit()
        //{
        //    return View();
        //}
        public IActionResult Delete()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Delete()
        //{
        //    return View();
        //}
    }
}
