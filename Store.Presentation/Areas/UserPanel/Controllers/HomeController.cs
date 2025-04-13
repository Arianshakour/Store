using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.UserPanel;
using System.Security.Claims;

namespace Store.Presentation.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var model = _userService.GetUserInformation(userId);
            return View(model);
        }
        public IActionResult EditProfile(int id)
        {
            var model = _userService.GetUserForEdit(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProfile(EditInfoDto edit)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var currentUser = _userService.GetUserForEdit(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"));
            //injoori ham mishod benevisi
            //var currentUser = _userService.GetUserForEdit(edit.Id);
            if (edit.Email != currentUser.Email && _userService.IsExistEmail(edit.Email))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نیست");
                return View(edit);
            }
            if (edit.UserName != currentUser.UserName && _userService.IsExistUserName(edit.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده در سیستم وجود دارد");
                return View(edit);
            }
            _userService.UpdateUserInfo(edit);
            return RedirectToAction("Index");
        }
        public IActionResult ChangePass(int id)
        {
            var model = _userService.GetUserForChangePass(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult ChangePass(ChangePassDto change)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!_userService.IsCorrectPass(change.Id,change.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "رمز عبور فعلی اشتباه است");
                return View(change);
            }
            _userService.ChangePassUser(change);
            return RedirectToAction("Index");
        }
        public IActionResult Wallet(int id)
        {
            var model = _userService.GetWallet(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Wallet(ChargeWalletDto charge)
        {
            return View();
        }
    }
}
