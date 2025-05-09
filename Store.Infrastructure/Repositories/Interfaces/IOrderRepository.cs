using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);
        Order GetOrderById(int id);
        List<Order> GetOrders();
        List<Order> GetOrdersWithTrue(int userId);
        void Save();
        Order? CheckOpenOrder(int userId);
        void UpdateOrderSumPrice(int orderId);
        Order GetOrderForDetails(int userId, int orderId);

        //OrderDetail
        void AddDetail(OrderDetail orderDetail);
        void UpdateDetail(OrderDetail orderDetail);
        void DeleteDetail(OrderDetail orderDetail);
        OrderDetail? GetByOrderId(int orderId, int productId);
    }
}
