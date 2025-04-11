using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.UserPanel
{
    public class ChangePassDto
    {
        public int Id { get; set; }
        [Display(Name = "رمز عبور فعلی")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = " رمز عبور جدید")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "فیلد {0} نباید خالی باشد")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "با رمز عبور مغایرت دارد")]
        public string RePassword { get; set; }
    }
}
