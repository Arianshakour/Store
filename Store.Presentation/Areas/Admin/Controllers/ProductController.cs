using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.AdminPanel.Product;
using Store.Domain.Entities;
using System.Security.Claims;

namespace Store.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var model = _productService.GetProducts();
            return View(model);
        }
        public IActionResult Create(int? gid)
        {
            var groupId = _productService.GetProductGroupsParent().productGroupList;
            ViewBag.GPar = new SelectList(groupId, "GroupId", "GroupTitle");
            //var subGroupId = _productService.GetProductGroupsSub(groupId.First().GroupId).productGroupList;
            if (gid.HasValue)
            {
                var subGroupIdajax = _productService.GetProductGroupsSub(gid.Value).productGroupList;
                return Json(new { productGroupList = subGroupIdajax });
            }
            var subGroupId = _productService.GetProductGroupsSub(groupId.First().GroupId).productGroupList;
            ViewBag.GSub = new SelectList(subGroupId, "GroupId", "GroupTitle");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductDto create)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GPar = new SelectList(_productService.GetProductGroupsParent().productGroupList, "GroupId", "GroupTitle",create.GroupId);

                return View(create);
            }
            create.UserId = int.Parse(((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            _productService.AddProduct(create);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var model = _productService.DetailsProduct(id);
            var groupId = _productService.GetProductGroupsParent().productGroupList;
            ViewBag.GPar = new SelectList(groupId, "GroupId", "GroupTitle",model.GroupId);
            var subGroupId = _productService.GetProductGroupsSub(model.GroupId).productGroupList;
            ViewBag.GSub = new SelectList(subGroupId, "GroupId", "GroupTitle",model.SubGroup);
            return View(model);
        }
        public IActionResult Edit(int id, int? gid)
        {
            var model = _productService.GetForEditProduct(id);
            var groupId = _productService.GetProductGroupsParent().productGroupList;
            ViewBag.GPar = new SelectList(groupId, "GroupId", "GroupTitle", model.GroupId);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" && gid.HasValue)
            {
                var subGroupIdajax = _productService.GetProductGroupsSub(gid.Value).productGroupList;
                return Json(new { productGroupList = subGroupIdajax });
            }
            var subGroupId = _productService.GetProductGroupsSub(model.GroupId).productGroupList;
            ViewBag.GSub = new SelectList(subGroupId, "GroupId", "GroupTitle", model.SubGroup);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditProductDto edit)
        {
            if (!ModelState.IsValid)
            {
                var groupId = _productService.GetProductGroupsParent().productGroupList;
                ViewBag.GPar = new SelectList(groupId, "GroupId", "GroupTitle", edit.GroupId);
                var subGroupId = _productService.GetProductGroupsSub(edit.GroupId).productGroupList;
                ViewBag.GSub = new SelectList(subGroupId, "GroupId", "GroupTitle", edit.SubGroup);
                return View(edit);
            }
            _productService.UpdateProduct(edit);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var model = _productService.GetForDeleteProduct(id);
            var groupId = _productService.GetProductGroupsParent().productGroupList;
            ViewBag.GPar = new SelectList(groupId, "GroupId", "GroupTitle", model.GroupId);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteProductDto delete)
        {
            _productService.DeleteProduct(delete);
            return RedirectToAction("Index");
        }
    }
}
