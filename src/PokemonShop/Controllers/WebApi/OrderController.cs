using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PokemonShop.DataTransferObjects;
using PokemonShop.Domain.Models;
using PokemonShop.Repository;
using PokemonShop.Services.MessageSender;
using PokemonShop.Services.Orders;
using PokemonShop.Services.Users;

namespace PokemonShop.Controllers.WebApi
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        #region Fields

        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IMessageSender _messageSenderService;
        private readonly IRepository<User> _userRepository;

        #endregion

        #region Ctor

        public OrderController(IOrderService orderService, 
            IUserService userService,
            IMessageSender messageSenderService,
            IRepository<User> userRepository)
        {
            _orderService = orderService;
            _userService = userService;
            _userRepository = userRepository;
            _messageSenderService = messageSenderService;
        }

        #endregion

        [HttpGet]
        public List<OrderHistoryItemDto> Get() // Get the order history of each user
        {
            var orderHistoryItems = _orderService.GetOrderHistoryItems();
            return orderHistoryItems;
        }

        [HttpPost]
        public void Post([FromBody]OrderDto orderDetails) // make new order
        {
            // get existing user or create new
            var user = _userRepository.Set.FirstOrDefault(x => x.Email == orderDetails.Email) ??
                       _userService.CreateUser(orderDetails.UserName, orderDetails.Email, orderDetails.PhoneNumber);

            // update user if name or phone number were changed
            if (user.Name != orderDetails.UserName || user.PhoneNumber != orderDetails.PhoneNumber)
            {
                _userService.UpdateUser(user.Id, orderDetails.UserName, orderDetails.PhoneNumber);
            }

            // create new order for this user
            _orderService.CreateOrder(user.Id);

            // send order information 
            _messageSenderService.PokemonOrdered(
                new UserDto {Email = user.Email, Name = user.Name, Phone = user.PhoneNumber},
                "Информация о заказе",
                "Покемон успешно заказан.");
        }
    }
}
