using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.AdminPanel.Product
{
    public class DeleteProductDto
    {
        public int ProductId { get; set; }
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductTitle { get; set; }
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int GroupId { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "آیا موجود است ؟")]
        public bool IsValid { get; set; }
        [Display(Name = "موجودی")]
        public int Mojodi { get; set; }
    }
}
