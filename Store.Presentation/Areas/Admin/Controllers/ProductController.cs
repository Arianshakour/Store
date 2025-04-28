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
            return View();
        }
    }
}
