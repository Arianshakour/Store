using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.AdminPanel.Product
{
    public class CreateProductDto
    {
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductTitle { get; set; }
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int GroupId { get; set; }
        //baraye ineke masalan dastebandie pesarane -> tshirt hast
        [Display(Name = "زیر شاخه")]
        public int? SubGroup { get; set; }
        [Display(Name = "کد کاربر ایجاد کننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserId { get; set; }
        //in baraye ine ke bbinim ki in mahsoolo ijad karde
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "تگ ها")]
        public string Tags { get; set; }
        [Display(Name = "آیا موجود است ؟")]
        public bool IsValid { get; set; }
        [Display(Name = "موجودی")]
        public int Mojodi { get; set; }
        [Display(Name = "تصویر")]
        public string? ImageName { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile ImgUp { get; set; }
        [Display(Name = "تاریخ درج")]
        public DateTime CreateDate { get; set; }
        public bool Dlt { get; set; }
    }
}
