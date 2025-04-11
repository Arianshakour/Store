using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.Login
{
    public class RegisterDto
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [EmailAddress(ErrorMessage = "قالب {0} اشتباه است")]
        public string Email { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "با رمز عبور مغایرت دارد")]
        public string RePassword { get; set; }

        //ino gozashtam baraye inke to ersak email model aslio pass nadam
        public string ActiveCode { get; set; } = string.Empty;
    }
}
