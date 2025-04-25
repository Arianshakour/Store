using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Application.Services.Interfaces;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.CustomAutorize
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private int _permissionId = 0;
        private IPermissionService _permissionService;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService =
                (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //string userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //mostaqim cast kardam be int
                int userId = int.Parse(((ClaimsPrincipal)context.HttpContext.User).FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (!_permissionService.CheckPermission(userId,_permissionId))
                {
                    //context.Result = new RedirectResult("/Login/login");
                    //context.Result = new RedirectResult("Admin/Error/Index");
                    //in balaeie url pak nemikardesh
                    context.HttpContext.Response.Redirect("/Admin/Error/Index");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login/Login");
            }
        }
    }
}
