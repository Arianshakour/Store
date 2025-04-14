using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.ViewModels
{
    public class UserViewModel
    {
        public User user { get; set; }
        public List<User> userList { get; set; }

        public UserViewModel()
        {
            user = new User();
            userList = new List<User>();
        }
    }
}
