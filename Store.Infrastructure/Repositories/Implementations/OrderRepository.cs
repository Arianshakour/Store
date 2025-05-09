using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infrastructure.Context;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyContext _context;
        public OrderRepository(MyContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public Order? CheckOpenOrder(int userId)
        {
            return _context.Orders.Include(x=>x.orderDetails).ThenInclude(x=>x.product)
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public Order GetOrderById(int id)
        {
            var o = _context.Orders.FirstOrDefault(x => x.OrderId == id);
            if (o == null)
            {
                throw new NullReferenceException();
            }
            return o;
        }

        public List<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public List<Order> GetOrdersWithTrue(int userId)
        {
            return _context.Orders.Include(x => x.orderDetails).ThenInclude(x => x.product)
                .Where(x=>x.UserId==userId && x.IsFinaly==true).ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
        public void UpdateOrderSumPrice(int orderId)
        {
            var o = GetOrderById(orderId);
            o.OrderSum = _context.OrderDetails.Where(x => x.OrderId == orderId).Sum(s => s.RowSum);
            Update(o);
            Save();
        }

        public Order GetOrderForDetails(int userId, int orderId)
        {
            var o = _context.Orders.Include(x => x.orderDetails).ThenInclude(x => x.product)
                .FirstOrDefault(x => x.UserId == userId && x.OrderId == orderId);
            if (o == null)
            {
                throw new NullReferenceException();
            }
            return o;
        }

        //OrderDetails

        public void AddDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
        }
        public void UpdateDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
        }
        public void DeleteDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Remove(orderDetail);
        }
        public OrderDetail? GetByOrderId(int orderId, int productId)
        {
            return _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.ProductId == productId);
        }
    }
}
