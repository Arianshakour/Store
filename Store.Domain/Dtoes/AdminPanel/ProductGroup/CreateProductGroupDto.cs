using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.AdminPanel.ProductGroup
{
    public class CreateProductGroupDto
    {
        [Display(Name = "نام سرگروه")]
        [Required(ErrorMessage = "عنوان سرگروه را وارد کنید")]
        public string GroupTitle { get; set; }
        // inja new List<SubGroupDto>() zadim ta khataye null nade
        public List<string>? subGroups { get; set; } = new List<string>();

    }
}
