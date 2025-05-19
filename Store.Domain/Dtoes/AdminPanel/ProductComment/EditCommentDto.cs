using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.AdminPanel.ProductComment
{
    public class EditCommentDto
    {
        public int CommentId { get; set; }
        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "وضعیت نمایش")]
        public bool IsShow { get; set; }
    }
}
