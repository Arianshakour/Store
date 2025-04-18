using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.ViewModels
{
    public class RoleViewModel
    {
        public Role role { get; set; }
        public List<Role> roleList { get; set; }

        public RoleViewModel()
        {
            role = new Role();
            roleList = new List<Role>();
        }
    }
}
