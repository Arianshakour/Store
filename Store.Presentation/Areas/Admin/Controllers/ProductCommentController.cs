using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.AdminPanel.ProductComment;

namespace Store.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCommentController : Controller
    {
        private readonly IProductCommentService _productCommentService;
        private readonly IProductService _productService;
        public ProductCommentController(IProductCommentService productCommentService, IProductService productService)
        {
            _productCommentService = productCommentService;
            _productService = productService;
        }
        public IActionResult Index(int productId=9)
        {
            ViewBag.ProductName = new SelectList(_productService.GetProducts().productList, "ProductId", "ProductTitle", productId);
            var model = _productCommentService.GetCommentsForProduct(productId);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_gridProductComment", model);
            }
            return View(model);
        }
        public IActionResult Details(int id)
        {
            var model = _productCommentService.GetCommentForDetails(id);
            return PartialView(model);
        }
        public IActionResult Edit(int id)
        {
            var model = _productCommentService.GetCommentForEdit(id);
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult Edit(EditCommentDto edit)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }
            _productCommentService.EditComment(edit);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var model = _productCommentService.GetCommentForDelete(id);
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteCommentDto delete)
        {
            _productCommentService.DeleteComment(delete);
            return RedirectToAction("Index");
        }
    }
}
