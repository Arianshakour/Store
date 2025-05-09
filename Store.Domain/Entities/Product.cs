using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        [Required]
        public int GroupId { get; set; }
        //baraye ineke masalan dastebandie pesarane -> tshirt hast
        public int? SubGroup { get; set; }
        [Required]
        public int UserId { get; set; }
        //in baraye ine ke bbinim ki in mahsoolo ijad karde
        [Required]
        public int Price { get; set; }
        public string Tags { get; set; }
        public bool IsValid { get; set; }
        public int Mojodi { get; set; }
        public string ImageName { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Dlt { get; set; }

        //relation
        //2 ta relation darim be ProductGroup chera??
        //avali daste bandi aslie hast masalan pesarane , dovomi zir shakhe hast masalan tshirt

        [ForeignKey("GroupId")]
        public ProductGroup productGroup { get; set; }
        [ForeignKey("SubGroup")]
        public ProductGroup subProductGroup { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        public List<OrderDetail> orderDetails { get; set; }


    }
}
