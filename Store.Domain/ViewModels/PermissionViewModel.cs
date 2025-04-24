using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.ViewModels
{
    public class PermissionViewModel
    {
        public Permission permission { get; set; }
        public List<Permission> permissionList { get; set; }

        public PermissionViewModel()
        {
            permission = new Permission();
            permissionList = new List<Permission>();
        }
    }
}
