using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Implementations;
using Store.Application.Services.Interfaces;
using Store.Domain.Entities;

namespace Store.Presentation.Components
{
    public class ListProductGroupViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public ListProductGroupViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _productService.GetProductGroups();
            return View("ListProductGroup", model);
        }
    }
}
