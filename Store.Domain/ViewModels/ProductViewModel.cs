using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.ViewModels
{
    public class ProductViewModel
    {
        public Product product { get; set; }
        public List<Product> productList { get; set; }
        public Pager pager { get; set; }

        public ProductViewModel()
        {
            product = new Product();
            productList = new List<Product>();
            pager = new Pager();
        }
    }
}
