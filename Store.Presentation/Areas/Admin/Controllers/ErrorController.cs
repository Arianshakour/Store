using Microsoft.AspNetCore.Mvc;

namespace Store.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
