using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.UserPanel
{
    public class SideBarUserPanelDto
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        public string UserName { get; set; }
        [Display(Name = "تاریخ عضویت")]
        public DateTime CreateOn { get; set; }
        [Display(Name = "تصویر")]
        public string? ImageName { get; set; }
    }
}
