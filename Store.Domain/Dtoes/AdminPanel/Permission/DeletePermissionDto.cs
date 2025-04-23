using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.AdminPanel.Permission
{
    public class DeletePermissionDto
    {
        public int RoleId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public string RoleTitle { get; set; }
    }
}
