using Microsoft.AspNetCore.Http;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.AdminPanel
{
    public class CreateUserDto
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تصویر")]
        public string? ImageName { get; set; }
        //baraye daryaft tasvir elzami ast ke in khat ziro benevisi
        [Display(Name = "تصویر")]
        public IFormFile? imgUp { get; set; }

        [Display(Name = "نقش ها")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public List<int> UserRoles { get; set; }
    }
}
