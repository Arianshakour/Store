using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.AdminPanel.ProductGroup;

namespace Store.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductGroupController : Controller
    {
        private readonly IProductService _productService;
        public ProductGroupController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var model = _productService.GetProductGroupsParent();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductGroupDto create)
        {
            if (!ModelState.IsValid)
            {
                return View(create);
            }
            _productService.AddProductGroup(create);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var model = _productService.GetProductGroupForEdit(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditProductGroupDto edit)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }
            _productService.UpdateProductGroup(edit);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
