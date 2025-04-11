using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.UserPanel
{
    public class InformationUserDto
    {
        [Display(Name = "نام")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "تاریخ عضویت")]
        public DateTime CreateOn { get; set; }
        [Display(Name = "کیف پول")]
        public int Wallet { get; set; }
    }
}
