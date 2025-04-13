using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.UserPanel
{
    public class ListWalletDto
    {
        [Display(Name = "مبلغ")]
        public int Amount { get; set; }
        [Display(Name = "نوع تراکنش")]
        public int Type { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime DateTime { get; set; }
    }
}
