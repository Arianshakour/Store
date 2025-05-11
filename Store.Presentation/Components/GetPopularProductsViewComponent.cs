using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;

namespace Store.Presentation.Components
{
    public class GetPopularProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public GetPopularProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _productService.GetPopularProducts();
            return View("GetPopularProducts", model);
        }
    }
}
