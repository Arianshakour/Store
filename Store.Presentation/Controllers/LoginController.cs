using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Store.Application.Services.Interfaces;
using Store.Domain.Common.Utilities;
using Store.Domain.Dtoes.Login;
using System.Security.Claims;
using Store.Domain.Common.Mail;
using Microsoft.SqlServer.Server;

namespace Store.Presentation.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            if (_userService.IsExistEmail(register.Email))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نیست");
                return View(register);
            }
            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری وارد شده در سیستم وجود دارد");
                return View(register);
            }
            _userService.AddUser(register);
            string bodyEmail = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/_ActiveEmail.cshtml", register);
            //SendEmail.Send(register.Email,"فعال سازی",bodyEmail);
            //in code bala kar nemikone vagarna email ham ersal mishe
            return View("SuccessRegister", register);
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            var user = _userService.LoginMember(login);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری با مشخصات یافت شده پیدا نشد");
                return View(login);
            }
            if (user.IsActive == false)
            {
                ModelState.AddModelError("Email", "حسابت فعال نیست");
                return View(login);
            }
            var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Email,user.Email)
                    };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };
            HttpContext.SignInAsync(principal, properties);
            ViewBag.IsSuccess = true;
            //return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login/Login");
        }
        public IActionResult ActiveAccount(string id)
        {
            //hatman bala string id benevis chon parameter aval bayad id bashe age benevisi string code null mishe
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }
        public IActionResult ForgotPass()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            }
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPass(ForgotPasswordDto forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }
            var user = _userService.GetUserByEmail(forgot.Email);
            if(user == null)
            {
                ModelState.AddModelError("Email", "کاربری یافت نشد");
                return View(forgot);
            }
            //string bodyEmail = ViewRendererUtils.RenderRazorViewToString(this, "~/Views/Login/_ForgotPassword.cshtml", user);
            //SendEmail.Send(user.Email, "بازیابی حساب کاربری", bodyEmail);
            //ViewBag.IsSuccess = true;
            //return View();
            return RedirectToAction("ResetPass", new { email = forgot.Email });
        }
        [HttpGet]
        public IActionResult ResetPass(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            }
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("ForgotPass");
            }
            return View(new ResetPasswordDto { Email = email });
        }
        [HttpPost]
        public IActionResult ResetPass(ResetPasswordDto reset)
        {
            if (!ModelState.IsValid)
            {
                return View(reset);
            }
            _userService.UpdatePassUser(reset);
            ViewBag.IsSuccess = true;
            return View(new ResetPasswordDto { Email = reset.Email });
        }
    }
}
