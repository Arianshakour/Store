using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class ProductGroup
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupTitle { get; set; }
        public bool Dlt { get; set; }
        public int? ParentId { get; set; }
        //in baraye zir shakhe hast
        //yek relation ham az jense list mikhad be khodesh ke yani chanta zir majmooe mitoone dashte bashe

        //relation
        [ForeignKey("ParentId")]
        public List<ProductGroup> productGroups { get; set; }

        //2ta mikhaim be product yki baraye groupid va digari zir shakhe
        [InverseProperty("productGroup")]
        public List<Product> group { get; set; }
        [InverseProperty("subProductGroup")]
        public List<Product> subGroup { get; set; }
    }
}
