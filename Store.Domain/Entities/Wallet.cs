using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }
        //behtare foreign key haro bbari to context moshakhas koni ta ye moqe qati nakone
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserId { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }

        [Display(Name = "تایید شده")]
        public bool IsPay { get; set; }

        [Display(Name = "زمان")]
        public DateTime CreateDate { get; set; }

        //be wallet be cheshme kif pol negah nakon
        //be wallet be cheshme tarakonesh negah kon pas yani ye user listi az tarakonesh haro dare
        public User user { get; set; }
        //az tarafi ya on tarakonesh variz boode ya bardasht pas ydone hast
        //relation user automat tashkhs dad vali
        //inja chon qati karde bood injoori neveshtam taze to CONTEXT ham neveshtam relation o
        [ForeignKey(nameof(WalletType))]
        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TypeId { get; set; }

        public WalletType walletType { get; set; }
    }
}
