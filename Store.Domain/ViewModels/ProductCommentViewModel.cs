using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.ViewModels
{
    public class ProductCommentViewModel
    {
        public ProductComment productComment { get; set; }
        public List<ProductComment> productCommentList { get; set; }


        public ProductCommentViewModel()
        {
            productComment = new ProductComment();
            productCommentList = new List<ProductComment>();
        }
    }
}
