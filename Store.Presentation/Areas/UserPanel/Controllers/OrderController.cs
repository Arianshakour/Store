using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;
using Store.Domain.Entities;
using System.Security.Claims;

namespace Store.Presentation.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index(int id)
        {
            var model = _orderService.GetOrderWithTrue(id);
            return View(model);
        }
        public IActionResult ShowOrder(int id)
        {
            var model = _orderService.GetOrderWithFalse(id);
            return View(model);
        }
        public IActionResult Payment(int id)
        {
            int userId = int.Parse(((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = _orderService.FinalyOrder(userId, id);
            ViewBag.Success = true;
            ViewBag.Id = userId;
            if (!result)
            {
                ViewBag.Success = false;
                return View();
            }
            return View();
        }
        public IActionResult OrderDetails(int id)
        {
            int userId = int.Parse(((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var model = _orderService.GetOrderForDetails(userId, id);
            return View(model);
        }
        public IActionResult UpdateOrderItem(int orderDetailId, int count)
        {
            int userId = int.Parse(((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            _orderService.UpdateOrderItem(orderDetailId, count);
            return RedirectToAction("ShowOrder", new { id = userId });
        }

        public IActionResult DeleteOrderItem(int detailId)
        {
            int userId = int.Parse(((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            _orderService.DeleteOrderItem(detailId);
            return RedirectToAction("ShowOrder", new { id = userId });
        }
    }
}
