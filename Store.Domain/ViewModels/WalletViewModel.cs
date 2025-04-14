using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.ViewModels
{
    public class WalletViewModel
    {
        public Wallet wallet { get; set; }
        public List<Wallet> walletList { get; set; }

        public WalletViewModel()
        {
            wallet = new Wallet();
            walletList = new List<Wallet>();
        }
    }
}
