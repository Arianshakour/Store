using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.ViewModels
{
    public class OrderViewModel
    {
        public Order order { get; set; }
        public List<Order> orderList { get; set; }

        public OrderViewModel()
        {
            order = new Order();
            orderList = new List<Order>();
        }
    }
}
