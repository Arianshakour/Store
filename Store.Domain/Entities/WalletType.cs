using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class WalletType
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "نوع تراکنش")]
        [MaxLength(150)]
        //manzooram variz ya bardasht ast
        public string TypeTitle { get; set; }


        //chon harkodom az variz ya bardasht mitoonan listi az tarakonesh ra dashte bashan
        public List<Wallet> wallets { get; set; }
    }
}
