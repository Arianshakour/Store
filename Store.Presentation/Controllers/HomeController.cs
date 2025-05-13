using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;
using Store.Presentation.Models;

namespace Store.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;
    private readonly IProductCommentService _productCommentService;
    public HomeController(IProductService productService, IOrderService orderService, IProductCommentService productCommentService)
    {
        _productService = productService;
        _orderService = orderService;
        _productCommentService = productCommentService;
    }
    public IActionResult Index()
    {
        var model = _productService.ShowLastProduct();
        return View(model);
    }
    public IActionResult Archive(string search, string type, string orderby,
            int startPrice, int endPrice, List<int> selectedGroups, int page = 1, int pageSize = 6)
    {
        var model = _productService.ShowAllProduct(search, type, orderby, startPrice, endPrice, selectedGroups, page, pageSize);
        ViewBag.groups = _productService.GetProductGroups().productGroupList;
        ViewBag.search = search ?? "";
        if (selectedGroups != null && selectedGroups.Any())
        {
            //ViewBag.pg = (_productService.GetProductGroup(selectedGroups.First())).productGroup.GroupTitle;
            var titles = _productService.GetGroupTitlesById(selectedGroups);
            ViewBag.pg = string.Join(",", titles);
        }
        else
        {
            ViewBag.pg = null;
        }
        return View(model);
    }
    public IActionResult ShowProduct(int id)
    {
        var model = _productService.ShowProduct(id);
        return View(model);
    }
    public IActionResult BuyProduct(int id)
    {
        int userId = int.Parse(((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        _orderService.AddOrder(userId, id);
        return RedirectToAction("ShowProduct", new { id = id });
    }
    public IActionResult AddComment(int ProductId,int UserId,string Comment)
    {
        _productCommentService.AddComment(ProductId,UserId,Comment);
        return RedirectToAction("ShowProduct", new { id = ProductId });
    }

    public IActionResult AutoCom(string term)
    {
        var model = _productService.GetSearchSuggestions(term);
        return Json(model);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
