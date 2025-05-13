using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Dtoes.AdminPanel.ProductGroup
{
    public class DeleteProductGroupDto
    {
        public int Id { get; set; }

        [Display(Name = "عنوان سرگروه")]
        [Required(ErrorMessage = "عنوان سرگروه را وارد کنید")]
        public string GroupTitle { get; set; }
        // inja new List<SubGroupDtoForDelete>() zadim ta khataye null nade
        public List<SubGroupDtoForDelete>? SubGroups { get; set; } = new List<SubGroupDtoForDelete>();
    }
    //chon to delete be id ham niaz darim nemishe mese create nevesht
    //bayad injoori tarif kardesh
    public class SubGroupDtoForDelete
    {
        public int? Id { get; set; }
        public string GroupTitle { get; set; }
    }
}
