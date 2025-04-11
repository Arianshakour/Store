using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.UserPanel
{
    public class EditInfoDto
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [EmailAddress(ErrorMessage = "قالب {0} اشتباه است")]
        public string Email { get; set; }
        [Display(Name = "تصویر")]
        public string UserAvatar { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile? imgUp { get; set; }
    }
}
