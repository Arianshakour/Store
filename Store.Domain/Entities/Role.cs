using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }


        public List<UserRole> userRoles { get; set; }
    }
}
