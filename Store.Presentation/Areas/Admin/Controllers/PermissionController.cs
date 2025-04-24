using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.AdminPanel.Permission;

namespace Store.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public IActionResult Index()
        {
            var model = _permissionService.GetRoles();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.pers = _permissionService.GetPermissions().permissionList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatePermissionDto create)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.pers = _permissionService.GetPermissions().permissionList;
                return View(create);
            }
            _permissionService.AddRole(create);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.pers = _permissionService.GetPermissions().permissionList;
            ViewBag.selectedPers = _permissionService.GetPermissionIdFromRoleId(id);
            var model = _permissionService.GetRoleByIdForEdit(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditPermissionDto edit)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.pers = _permissionService.GetPermissions().permissionList;
                ViewBag.selectedPers = _permissionService.GetPermissionIdFromRoleId(edit.RoleId);
                return View(edit);
            }
            _permissionService.UpdateRole(edit);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            ViewBag.pers = _permissionService.GetPermissions().permissionList;
            ViewBag.selectedPers = _permissionService.GetPermissionIdFromRoleId(id);
            var model = _permissionService.GetRoleByIdForDelete(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeletePermissionDto delete)
        {
            _permissionService.DeleteRole(delete);
            return RedirectToAction("Index");
        }
    }
}
