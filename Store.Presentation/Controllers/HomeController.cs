using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;
using Store.Presentation.Models;

namespace Store.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    public HomeController(IProductService productService)
    {
        _productService = productService;
    }
    public IActionResult Index()
    {
        var model = _productService.ShowLastProduct();
        return View(model);
    }
    public IActionResult Archive(string search, string type, string orderby,
            int startPrice, int endPrice, List<int> selectedGroups, int page =1, int pageSize =6)
    {
        var model = _productService.ShowAllProduct(search,type,orderby,startPrice,endPrice,selectedGroups,page,pageSize);
        ViewBag.groups = _productService.GetProductGroups().productGroupList;
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
