using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using PokemonShop.DataTransferObjects;
using PokemonShop.Domain.Models;
using PokemonShop.Repository;

namespace PokemonShop.Services.Orders
{
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Order> _orderRepository;

        #endregion

        #region Ctor

        public OrderService(IRepository<User> userRepository, IRepository<Order> orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        #endregion

        public void CreateOrder(int userId)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now
            };

            _orderRepository.Insert(order);
        }

        public List<OrderHistoryItemDto> GetOrderHistoryItems()
        {
            var orderHistoryItems = _userRepository.Set.Select(x => new OrderHistoryItemDto
            {
                UserName = x.Name,
                OrderDate = x.Orders.Select(o => o.OrderDate).OrderByDescending(d => d).First(),
                Count = x.Orders.Count
            }).OrderByDescending(x => x.OrderDate).ToList();

            return orderHistoryItems;
        }
    }
}