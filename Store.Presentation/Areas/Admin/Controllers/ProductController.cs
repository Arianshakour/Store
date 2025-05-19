using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Services.Interfaces;
using Store.Domain.Common.Utilities;
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
        public IActionResult Index(string searchValue, int page = 1, int pageSize = 4)
        {
            var model = _productService.GetProductsInAdmin(searchValue, page, pageSize);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("~/areas/admin/Views/Product/_gridProduct.cshtml", model);
            }
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
            return PartialView();
        }
        [HttpPost]
        public IActionResult Create(CreateProductDto create)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GPar = new SelectList(_productService.GetProductGroupsParent().productGroupList, "GroupId", "GroupTitle",create.GroupId);
                if (create.GroupId != 0)
                {
                    var subGroups = _productService.GetProductGroupsSub(create.GroupId).productGroupList;
                    ViewBag.GSub = new SelectList(subGroups,"GroupId","GroupTitle",create.SubGroup);
                }
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Areas/Admin/Views/Product/Create.cshtml", create);
                return Json(new { view = v1, success = false });
                //return View(create);
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
            return PartialView(model);
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
            return PartialView(model);
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
                string v1 = ViewRendererUtils.RenderRazorViewToString(this, "~/Areas/Admin/Views/Product/Edit.cshtml", edit);
                return Json(new { view = v1, success = false });
                //return View(edit);
            }
            _productService.UpdateProduct(edit);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var model = _productService.GetForDeleteProduct(id);
            var groupId = _productService.GetProductGroupsParent().productGroupList;
            ViewBag.GPar = new SelectList(groupId, "GroupId", "GroupTitle", model.GroupId);
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteProductDto delete)
        {
            _productService.DeleteProduct(delete);
            return RedirectToAction("Index");
        }
    }
}
