﻿using Store.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(int userId,int productId, int count);
        OrderViewModel GetOrderWithFalse(int userid);
        OrderViewModel GetOrderWithTrue(int userid);
        OrderViewModel GetOrderForDetails(int userId,int orderId);
        bool FinalyOrder(int userId,int orderId);
        void UpdateOrderItem(int orderDetailId, int count);
        void DeleteOrderItem(int orderDetailId);
    }
}
