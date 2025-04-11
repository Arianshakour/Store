using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Interfaces;
using System.Security.Claims;

namespace Store.Presentation.Areas.UserPanel.Components
{
    public class SideBarUserPanelViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        public SideBarUserPanelViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int userId = int.Parse(((ClaimsPrincipal)User).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var model = _userService.GetSideBarUserPanelData(userId);
            return View("SideBarUserPanel", model);
        }
    }
}
