using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.UserPanel;
using Store.Domain.Entities;
using Store.Domain.ViewModels;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUserService userService, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userService = userService;
            _userRepository = userRepository;
        }

        public void AddOrder(int userId, int productId)
        {
            var check = _orderRepository.CheckOpenOrder(userId);
            var p = _productRepository.GetProductById(productId);
            if (check == null)
            {
                var o = new Order()
                {
                    UserId = userId,
                    IsFinaly = false,
                    CreateDate = DateTime.Now,
                    OrderSum = p.Price,
                    orderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            ProductId = productId,
                            Count = 1,
                            Price = p.Price,
                        }
                    }
                };
                foreach (var orderDetail in o.orderDetails)
                {
                    orderDetail.JameRadif();
                }
                _orderRepository.Add(o);
                _orderRepository.Save();
            }
            else
            {
                var od = _orderRepository.GetByOrderId(check.OrderId, productId);
                if(od != null)
                {
                    od.Count += 1;
                    od.JameRadif();
                    _orderRepository.UpdateDetail(od);
                }
                else
                {
                    od = new OrderDetail()
                    {
                        OrderId = check.OrderId,
                        Count = 1,
                        Price = p.Price,
                        ProductId = p.ProductId
                    };
                    od.JameRadif();
                    _orderRepository.AddDetail(od);
                }
                _orderRepository.Save();
                _orderRepository.UpdateOrderSumPrice(check.OrderId);
            }
        }

        public OrderViewModel GetOrderWithFalse(int userid)
        {
            var data = _orderRepository.CheckOpenOrder(userid);
            return new OrderViewModel()
            {
                order = data
            };
        }

        public bool FinalyOrder(int userId, int orderId)
        {
            var order = _orderRepository.CheckOpenOrder(userId);
            if (order == null)
            {
                return false;
            }
            if(_userService.GetMojodiWallet(userId) >= order.OrderSum)
            {
                order.IsFinaly = true;
                _userRepository.AddWallet(new Wallet()
                {
                    Amount = order.OrderSum,
                    CreateDate = DateTime.Now,
                    IsPay = true,
                    UserId = userId,
                    TypeId = 2,
                    Description = "فاکتور شما #" + order.OrderId
                });
                _orderRepository.Update(order);
                _orderRepository.Save();
                return true;
            }
            return false;
        }

        public OrderViewModel GetOrderWithTrue(int userid)
        {
            var data = _orderRepository.GetOrdersWithTrue(userid);
            return new OrderViewModel()
            {
                orderList = data
            };
        }

        public OrderViewModel GetOrderForDetails(int userId, int orderId)
        {
            var data = _orderRepository.GetOrderForDetails(userId, orderId);
            return new OrderViewModel()
            {
                order = data
            };
        }
    }
}
