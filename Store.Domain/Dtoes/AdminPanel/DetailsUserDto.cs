using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.AdminPanel
{
    public class DetailsUserDto
    {
        public int Id { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "تصویر")]
        public string? ImageName { get; set; }
        //baraye daryaft tasvir elzami ast ke in khat ziro benevisi
        [Display(Name = "تصویر")]
        public IFormFile? imgUp { get; set; }

        [Display(Name = "نقش ها")]
        public List<int> UserRoles { get; set; }
    }
}
