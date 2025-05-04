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
    public IActionResult Archive()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
