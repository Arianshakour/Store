using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.AdminPanel.ProductGroup
{
    public class EditProductGroupDto
    {
        public int Id { get; set; }

        [Display(Name = "عنوان سرگروه")]
        [Required(ErrorMessage = "عنوان سرگروه را وارد کنید")]
        public string GroupTitle { get; set; }
        // inja new List<SubGroupDto>() zadim ta khataye null nade
        public List<SubGroupDto>? SubGroups { get; set; } = new List<SubGroupDto>();
    }
    //chon to edit be id ham niaz darim nemishe mese create nevesht
    //bayad injoori tarif kardesh
    public class SubGroupDto
    {
        public int? Id { get; set; }
        public string GroupTitle { get; set; }
    }
}
