using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;
using System.Security.Claims;

namespace Store.Presentation.Areas.UserPanel.Components
{
    public class ListWalletViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        public ListWalletViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int userId = int.Parse(((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var model = _userService.GetWalletUser(userId);
            return View("ListWallet", model);
        }
    }
}
