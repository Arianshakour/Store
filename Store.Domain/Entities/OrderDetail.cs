using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int Price { get; set; }
        public int RowSum { get; set; }


        public void JameRadif()
        {
            RowSum = Count * Price;
        }
        //Relation
        [ForeignKey("OrderId")]
        public Order order { get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }
    }
}
