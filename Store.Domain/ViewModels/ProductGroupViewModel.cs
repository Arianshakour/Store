using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.ViewModels
{
    public class ProductGroupViewModel
    {
        public ProductGroup productGroup { get; set; }
        public List<ProductGroup> productGroupList { get; set; }

        public ProductGroupViewModel()
        {
            productGroup = new ProductGroup();
            productGroupList = new List<ProductGroup>();
        }
    }
}
