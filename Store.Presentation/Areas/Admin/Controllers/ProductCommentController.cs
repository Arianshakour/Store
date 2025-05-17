using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;

namespace Store.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCommentController : Controller
    {
        private readonly IProductCommentService _productCommentService;
        public ProductCommentController(IProductCommentService productCommentService)
        {
            _productCommentService = productCommentService;
        }
        public IActionResult Index(int productId=9)
        {
            var model = _productCommentService.GetCommentsForProduct(productId);
            return View(model);
        }
    }
}
